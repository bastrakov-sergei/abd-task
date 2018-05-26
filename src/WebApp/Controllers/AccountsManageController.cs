using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using WebApp.Models;
using WebApp.Services.Account;
using WebApp.ViewModels.AccountsManage;

namespace WebApp.Controllers
{
    [Authorize(Roles = Roles.Administrator)]
    public class AccountsManageController : Controller
    {
        private readonly IAccountService accountService;

        public AccountsManageController(IAccountService accountService)
        {
            this.accountService = accountService;
        }

        [HttpGet]
        public async Task<IActionResult> Index() => View(await accountService.GetUsersAsync().ConfigureAwait(false));

        [HttpGet]
        public IActionResult Add() => View();

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Add(AddUserViewModel model)
        {
            if (!ModelState.IsValid)
                return View(model);

            var (_, errors) = await accountService.AddAsync(model).ConfigureAwait(false);
            if (errors != Error.NoError)
                return this.ErrorView(errors);

            return RedirectToAction("Index");
        }

        [HttpGet]
        public async Task<IActionResult> Edit(string userId)
        {
            ApplicationUser user;
            string role;
            Error[] errors;

            (user, errors) = await accountService.GetUserAsync(userId).ConfigureAwait(false);
            if (errors != Error.NoError)
                return this.ErrorView(errors);

            (role, errors) = await accountService.GetRoleAsync(user).ConfigureAwait(false);

            if (errors != Error.NoError)
                return this.ErrorView(errors);

            return View(new EditUserViewModel
            {
                Id = user.Id,
                Login = user.UserName,
                Role = role
            });
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(EditUserViewModel model)
        {
            if (!ModelState.IsValid)
                return View(model);

            var (_, errors) = await accountService.UpdateAsync(model).ConfigureAwait(false);
            return errors != Error.NoError ? 
                this.ErrorView(errors) : 
                RedirectToAction("Index");
        }

        [HttpGet]
        public async Task<IActionResult> Delete(string userId)
        {
            var (user, errors) = await accountService.GetUserAsync(userId).ConfigureAwait(false);
            return errors != Error.NoError ?
                this.ErrorView(errors) :
                View(user);
        }

        [HttpPost]
        [ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(string userId)
        {
            var (user, errors) = await accountService.GetUserAsync(userId).ConfigureAwait(false);
            if (errors != Error.NoError)
                return this.ErrorView(errors);

            if (await accountService.IsSame(user, HttpContext.User).ConfigureAwait(false))
                return this.ErrorView(Error.Create("DeleteError", "Can not delete self."));

            errors = await accountService.DeleteAsync(user).ConfigureAwait(false);
            return errors != Error.NoError ?
                this.ErrorView(errors) :
                RedirectToAction("Index");
        }
    }
}