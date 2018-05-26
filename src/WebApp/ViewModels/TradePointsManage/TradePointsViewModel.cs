using System.Diagnostics.CodeAnalysis;
using WebApp.Models.TradePoints;
using X.PagedList;

namespace WebApp.ViewModels.TradePointsManage
{
    [SuppressMessage("ReSharper", "NotNullMemberIsNotInitialized")]
    public class TradePointsViewModel
    {
        public IPagedList<TradePoint> TradePoints { get; set; }
    }
}
