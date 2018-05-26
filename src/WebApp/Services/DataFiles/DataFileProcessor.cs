using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Hangfire;
using JetBrains.Annotations;
using Microsoft.EntityFrameworkCore;
using WebApp.Data;
using WebApp.Models;
using WebApp.Models.DataFiles;
using WebApp.Models.TradePoints;

namespace WebApp.Services.DataFiles
{
    public sealed class DataFileProcessor : IDataFileProcessor
    {
        private readonly IEnumerable<IDataFileHandler> handlers;
        private readonly ApplicationDbContext applicationDbContext;

        public DataFileProcessor(
            IEnumerable<IDataFileHandler> handlers, 
            ApplicationDbContext applicationDbContext)
        {
            this.handlers = handlers;
            this.applicationDbContext = applicationDbContext;
        }

        [ItemCanBeNull]
        public async Task<Error[]> HandleAsync(Guid dataFileId)
        {
            var dataFile = await applicationDbContext.DataFiles.FirstAsync(df => df.Id == dataFileId).ConfigureAwait(false);
            if (dataFile == null)
                return Error.Create("DataFileNotFound", "");

            dataFile.IsProcessed = false;

            var errors = await HandleInternalAsync(dataFile).ConfigureAwait(false);
            dataFile.ProcessingError =
                errors != null ? string.Join(";", errors.Select(e => $"{e.Code}|{e.Description}")) : null;
            applicationDbContext.DataFiles.Update(dataFile);

            await applicationDbContext.SaveChangesAsync().ConfigureAwait(false);
            return errors;
        }

        [ItemCanBeNull]
        private async Task<Error[]> HandleInternalAsync(DataFile dataFile)
        {
            var handler = await FindHandler(dataFile).ConfigureAwait(false);
            if (handler == null)
                return Error.Create("HandlerNotFound", $"Can not find handler for file with Id={dataFile.Id}.");

            var (tradePoints, errors) = await handler.HandleAsync(dataFile).ConfigureAwait(false);
            if (errors != Error.NoError)
                return errors;

            dataFile.Type = handler.DataFileType;
            return await SaveAsync(tradePoints, dataFile).ConfigureAwait(false);
        }

        [ItemCanBeNull]
        private async Task<Error[]> SaveAsync(IEnumerable<TradePoint> loadedTradePoints, DataFile dataFile)
        {
            foreach (var loadedTradePoint in loadedTradePoints)
            {
                var tradePointSource = await applicationDbContext.TradePointSources
                    .Include(source => source.TradePoint)
                    .Include(source => source.TradePoint.Type)
                    .Include(source => source.TradePoint.Location)
                    .FirstOrDefaultAsync(source =>
                        source.TradePointSourceId == loadedTradePoint.Id &&
                        source.DataFileType == dataFile.Type).ConfigureAwait(false);

                var type = await GetOrCreateType(loadedTradePoint).ConfigureAwait(false);

                var needCreate = tradePointSource == null;

                if (needCreate)
                    tradePointSource = new TradePointSource
                    {
                        TradePoint = new TradePoint
                        {
                            Id = Guid.NewGuid(),
                            Location = new Location()
                        }
                    };

                tradePointSource.DataFileType = dataFile.Type ?? throw new InvalidOperationException();
                tradePointSource.TradePointSourceId = loadedTradePoint.Id;
                tradePointSource.TradePoint.Name = loadedTradePoint.Name;
                tradePointSource.TradePoint.Type = type;
                tradePointSource.TradePoint.Location.Latitude = loadedTradePoint.Location.Latitude;
                tradePointSource.TradePoint.Location.Longitude = loadedTradePoint.Location.Longitude;

                if (needCreate)
                    await applicationDbContext.TradePointSources.AddAsync(tradePointSource).ConfigureAwait(false);
                else
                    applicationDbContext.TradePointSources.Update(tradePointSource);
            }

            dataFile.IsProcessed = true;
            await applicationDbContext.SaveChangesAsync().ConfigureAwait(false);
            return Error.NoError;
        }

        private async Task<TradePointType> GetOrCreateType(TradePoint loadedTradePoint)
        {
            var type = await applicationDbContext.TradePointTypes
                           .FirstOrDefaultAsync(t => t.Name == loadedTradePoint.Type.Name).ConfigureAwait(false) ??
                       new TradePointType
                       {
                           Id = Guid.NewGuid(),
                           Name = loadedTradePoint.Type.Name
                       };

            return type;
        }

        [Obsolete("Public only for hangfire")]
        public async Task HandleInternalAsync(Guid dataFileId)
        {
            var errors = await HandleAsync(dataFileId).ConfigureAwait(false);
            if (errors != Error.NoError)
                throw new ApplicationException().AddData(errors);
        }

        [ItemCanBeNull]
        private async Task<IDataFileHandler> FindHandler(DataFile dataFile)
        {
            foreach (var handler in handlers)
            {
                if (await handler.CanHandleAsync(dataFile).ConfigureAwait(false))
                    return handler;
            }

            return null;
        }

        public static void Enqueue(Guid dataFileId)
        {
#pragma warning disable 618
            BackgroundJob.Enqueue<DataFileProcessor>(processor => processor.HandleInternalAsync(dataFileId));
#pragma warning restore 618
        }
    }
}