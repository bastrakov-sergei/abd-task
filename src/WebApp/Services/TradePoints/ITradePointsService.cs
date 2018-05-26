using System;
using System.Threading.Tasks;
using JetBrains.Annotations;
using WebApp.Models;
using WebApp.Models.TradePoints;
using WebApp.ViewModels.TradePointsManage;
using X.PagedList;

namespace WebApp.Services.TradePoints
{
    public interface ITradePointsService
    {
        Task<(TradePoint, Error[])> AddAsync(AddTradePointViewModel viewModel);
        Task<(IPagedList<TradePoint>, Error[])> GetAsync(int pageSize, int page);
        Task<(TradePoint, Error[])> GetAsync(Guid id);
        Task<TradePointType[]> GetTypesAsync();
        Task<(TradePoint, Error[])> UpdateAsync(EditTradePointViewModel viewModel);
        [ItemCanBeNull]
        Task<Error[]> DeleteAsync(TradePoint tradePoint);
    }
}