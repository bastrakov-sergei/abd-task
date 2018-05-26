using System;
using System.Linq;
using System.Security.Cryptography;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using WebApp.Data;
using WebApp.Models;
using WebApp.Models.DataFiles;
using WebApp.ViewModels;

namespace WebApp.Services.DataFiles
{
    public sealed class DataFilesStore : IDataFilesStore
    {
        private readonly ApplicationDbContext applicationDbContext;
        private readonly HashAlgorithm hashAlgorithm;

        public DataFilesStore(
            ApplicationDbContext applicationDbContext,
            HashAlgorithm hashAlgorithm)
        {
            this.applicationDbContext = applicationDbContext;
            this.hashAlgorithm = hashAlgorithm;
        }

        public async Task<(DataFile, Error[])> SaveAsync(UploadDataFileViewModel viewModel)
        {
            var fileHash = hashAlgorithm.ComputeHash(viewModel.Content);

            if (await applicationDbContext.DataFiles.AnyAsync(df => df.Hash == fileHash).ConfigureAwait(false))
                return (null, Error.Create(DataFilesStoreErrors.DataFileExists));

            var dataFile = new DataFile
            {
                Id = Guid.NewGuid(),
                Name = viewModel.Name,
                Content = viewModel.Content,
                Hash = fileHash,
                IsProcessed = false
            };

            await applicationDbContext.DataFiles.AddAsync(dataFile).ConfigureAwait(false);
            await applicationDbContext.SaveChangesAsync().ConfigureAwait(false);

            return (dataFile, Error.NoError);
        }

        public async Task<(DataFile, Error[])> GetAsync(Guid id) => (await applicationDbContext.FindAsync<DataFile>(id).ConfigureAwait(false), Error.NoError);

        public async Task<DataFile[]> GetUnprocessedAsync(bool includeContent = false)
        {
            var unprocessed = applicationDbContext.DataFiles.Where(df => !df.IsProcessed);
            var dataFilesQuery = includeContent
                ? unprocessed.Select(df =>
                    new DataFile
                    {
                        Id = df.Id,
                        Hash = df.Hash,
                        Name = df.Name,
                        ProcessingError = df.ProcessingError,
                        Content = df.Content
                    })
                : unprocessed
                    .Select(df => new DataFile
                    {
                        Id = df.Id,
                        Hash = df.Hash,
                        Name = df.Name,
                        ProcessingError = df.ProcessingError
                    });

            return await dataFilesQuery.ToArrayAsync().ConfigureAwait(false);
        }
    }
}

