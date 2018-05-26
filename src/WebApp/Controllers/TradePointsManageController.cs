using System;
using System.IO;
using System.Threading.Tasks;
using JetBrains.Annotations;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using WebApp.Controllers.TradePointsManage;
using WebApp.Models;
using WebApp.Services.DataFiles;
using WebApp.Services.TradePoints;
using WebApp.ViewModels;
using WebApp.ViewModels.TradePointsManage;

namespace WebApp.Controllers
{
    public class TradePointsManageController : Controller
    {
        private readonly ITradePointsService tradePointsService;
        private readonly IDataFilesStore dataFilesStore;

        public TradePointsManageController(
            ITradePointsService tradePointsService,
            IDataFilesStore dataFilesStore)
        {
            this.tradePointsService = tradePointsService;
            this.dataFilesStore = dataFilesStore;
        }

        [HttpGet]
        public async Task<IActionResult> Index(int page = 1)
        {
            var (tradePoints, errors) = await tradePointsService.GetAsync(10, page).ConfigureAwait(false);
            if (errors != Error.NoError)
                return this.ErrorView(errors);

            var dataFiles = await dataFilesStore.GetUnprocessedAsync().ConfigureAwait(false);

            var viewModel = new IndexViewModel
            {
                TradePoints = tradePoints,
                DataFiles = dataFiles
            };

            return View(viewModel);
        }

        [HttpGet]
        public async Task<IActionResult> Add()
        {
            ViewBag.Types = await tradePointsService.GetTypesAsync().ConfigureAwait(false);
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Add(AddTradePointViewModel viewModel)
        {
            if (!ModelState.IsValid)
            {
                ViewBag.Types = await tradePointsService.GetTypesAsync().ConfigureAwait(false);
                return View(viewModel);
            }

            var (_, errors) = await tradePointsService.AddAsync(viewModel).ConfigureAwait(false);
            return errors != null ?
                this.ErrorView(errors) :
                RedirectToAction("Index");
        }

        [HttpGet]
        public async Task<IActionResult> Edit(Guid tradePointId)
        {
            var (tradePoint, errors) = await tradePointsService.GetAsync(tradePointId).ConfigureAwait(false);
            if (errors != Error.NoError)
                return this.ErrorView(errors);

            ViewBag.Types = await tradePointsService.GetTypesAsync().ConfigureAwait(false);
            return View(new EditTradePointViewModel
            {
                Id = tradePoint.Id,
                TypeId = tradePoint.Type.Id,
                Latitude = tradePoint.Location.Latitude,
                Longitude = tradePoint.Location.Longitude,
                Name = tradePoint.Name
            });
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(EditTradePointViewModel viewModel)
        {
            if (!ModelState.IsValid)
                return View(viewModel);

            var (_, errors) = await tradePointsService.UpdateAsync(viewModel).ConfigureAwait(false);
            return errors != Error.NoError ?
                this.ErrorView(errors) :
                RedirectToAction("Index");
        }

        [HttpGet]
        public async Task<IActionResult> Delete(Guid tradePointId)
        {
            var (tradePoint, errors) = await tradePointsService.GetAsync(tradePointId).ConfigureAwait(false);
            return errors != Error.NoError ?
                this.ErrorView(errors) :
                View(tradePoint);
        }

        [HttpPost]
        [ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(Guid tradePointId)
        {
            var (user, errors) = await tradePointsService.GetAsync(tradePointId).ConfigureAwait(false);
            if (errors != Error.NoError)
                return this.ErrorView(errors);
            
            errors = await tradePointsService.DeleteAsync(user).ConfigureAwait(false);
            return errors != Error.NoError ?
                this.ErrorView(errors) :
                RedirectToAction("Index");
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Upload([CanBeNull] IFormFile uploadedFile)
        {
            if (uploadedFile == null)
                return this.ErrorView(Error.Create("EmptyFile", "File is required"));

            using (var binaryReader = new BinaryReader(uploadedFile.OpenReadStream()))
            {
                var viewModel = new UploadDataFileViewModel
                {
                    Name = uploadedFile.FileName,
                    Content = binaryReader.ReadBytes((int)uploadedFile.Length)
                };
                var (dataFile, errors) = await dataFilesStore.SaveAsync(viewModel).ConfigureAwait(false);
                if (errors != Error.NoError)
                    return this.ErrorView(errors);

                DataFileProcessor.Enqueue(dataFile.Id);
            }
            return RedirectToAction("Index");
        }
    }
}