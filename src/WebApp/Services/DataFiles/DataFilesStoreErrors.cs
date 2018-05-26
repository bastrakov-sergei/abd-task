using WebApp.Models;

namespace WebApp.Services.DataFiles
{
    public static class DataFilesStoreErrors
    {
        public static Error DataFileExists => new Error(nameof(DataFileExists), "Data file is exists");
    }
}