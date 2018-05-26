using WebApp.Models.DataFiles;
using WebApp.Models.TradePoints;
using X.PagedList;

namespace WebApp.Controllers.TradePointsManage
{
    public sealed class IndexViewModel
    {
        public IPagedList<TradePoint> TradePoints { get; set; }
        public DataFile[] DataFiles { get; set; }
    }
}