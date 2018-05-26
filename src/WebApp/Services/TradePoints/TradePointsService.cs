using System;
using System.Threading.Tasks;
using JetBrains.Annotations;
using Microsoft.EntityFrameworkCore;
using WebApp.Data;
using WebApp.Models;
using WebApp.Models.TradePoints;
using WebApp.ViewModels.TradePointsManage;
using X.PagedList;

namespace WebApp.Services.TradePoints
{
    public sealed class TradePointsService : ITradePointsService
    {
        private readonly ApplicationDbContext applicationDbContext;

        public TradePointsService(ApplicationDbContext applicationDbContext) => this.applicationDbContext = applicationDbContext;

        public async Task<(TradePoint, Error[])> AddAsync(AddTradePointViewModel viewModel)
        {
            return await applicationDbContext.DoTransaction(async () =>
            {
                if (await applicationDbContext.TradePoints.AnyAsync(tp => tp.Name == viewModel.Name).ConfigureAwait(false))
                    return (null, Error.Create(TradePointsServiceErrors.TradePointExists));

                var type = await applicationDbContext.TradePointTypes.FindAsync(viewModel.TypeId).ConfigureAwait(false);
                if (type == null)
                    return (null, Error.Create(TradePointsServiceErrors.TradePointTypeNotFound));

                var tradePoint = new TradePoint
                {
                    Id = Guid.NewGuid(),
                    Name = viewModel.Name,
                    Type = type,
                    Location = new Location
                    {
                        Latitude = viewModel.Latitude,
                        Longitude = viewModel.Longitude
                    }
                };

                var entry = await applicationDbContext.AddAsync(tradePoint).ConfigureAwait(false);
                await applicationDbContext.SaveChangesAsync().ConfigureAwait(false);

                return (entry.Entity, Error.NoError);
            }).ConfigureAwait(false);
        }

        public async Task<(IPagedList<TradePoint>, Error[])> GetAsync(int pageSize, int pageNumber = 0) => (await applicationDbContext.TradePoints.Include(tp => tp.Type).ToPagedListAsync(pageNumber, pageSize).ConfigureAwait(false), null);

        public async Task<(TradePoint, Error[])> GetAsync(Guid id)
        {
            var entity = await applicationDbContext.TradePoints.Include(tp => tp.Type).FirstOrDefaultAsync(tp => tp.Id == id).ConfigureAwait(false);
            return entity == null
                ? (null, Error.Create(TradePointsServiceErrors.TradePointNotFound))
                : (entity, Error.NoError);
        }

        public async Task<TradePointType[]> GetTypesAsync() => await applicationDbContext.TradePointTypes.ToArrayAsync().ConfigureAwait(false);

        public async Task<(TradePoint, Error[])> UpdateAsync(EditTradePointViewModel viewModel)
        {
            return await applicationDbContext.DoTransaction(async () =>
            {
                var tradePoint = await applicationDbContext.TradePoints.Include(tp => tp.Type).FirstOrDefaultAsync(tp => tp.Id == viewModel.Id).ConfigureAwait(false);
                if (tradePoint == null)
                    return (null, Error.Create(TradePointsServiceErrors.TradePointNotFound));

                var type = await applicationDbContext.TradePointTypes.FindAsync(viewModel.TypeId).ConfigureAwait(false);
                if (type == null)
                    return (null, Error.Create(TradePointsServiceErrors.TradePointTypeNotFound));

                tradePoint.Name = viewModel.Name;
                tradePoint.Type = type;
                tradePoint.Location.Latitude = viewModel.Latitude;
                tradePoint.Location.Longitude = viewModel.Longitude;

                applicationDbContext.Update(tradePoint);
                await applicationDbContext.SaveChangesAsync().ConfigureAwait(false);

                return (tradePoint, Error.NoError);
            }).ConfigureAwait(false);
        }

        [ItemCanBeNull]
        public async Task<Error[]> DeleteAsync(TradePoint tradePoint)
        {
            applicationDbContext.Set<TradePoint>().Remove(tradePoint);
            await applicationDbContext.SaveChangesAsync(false).ConfigureAwait(false);
            return Error.NoError;
        }
    }
}
