using Microsoft.AspNetCore.Mvc;

namespace WebApp.Controllers
{
    public class HomeController : Controller
    {
        public IActionResult Index() => View();
        
        public IActionResult Error() => this.ErrorView();
    }
}
