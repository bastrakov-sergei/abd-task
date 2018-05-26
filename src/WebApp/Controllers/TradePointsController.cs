using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using WebApp.Data;
using WebApp.Models.TradePoints;

namespace WebApp.Controllers
{
    public class TradePointsController : Controller
    {
        private readonly ApplicationDbContext applicationDbContext;

        public TradePointsController(ApplicationDbContext applicationDbContext)
        {
            this.applicationDbContext = applicationDbContext;
        }

        public IActionResult Index() => View();

        [HttpGet]
        public async Task<IActionResult> GetNearestTradePoints(double latitude, double longitude, double radius) => Ok(
            await applicationDbContext.GetNearestAsync(new Location
            {
                Latitude = latitude,
                Longitude = longitude
            }, radius).ConfigureAwait(false));
    }
}