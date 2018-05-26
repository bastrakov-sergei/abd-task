using System;
using System.Threading.Tasks;
using WebApp.Models;
using WebApp.Models.DataFiles;
using WebApp.ViewModels;

namespace WebApp.Services.DataFiles
{
    public interface IDataFilesStore
    {
        Task<(DataFile, Error[])> GetAsync(Guid id);
        Task<DataFile[]> GetUnprocessedAsync(bool includeContent = false);
        Task<(DataFile, Error[])> SaveAsync(UploadDataFileViewModel viewModel);
    }
}