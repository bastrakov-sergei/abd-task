using System.Diagnostics;
using JetBrains.Annotations;
using Microsoft.AspNetCore.Mvc;
using WebApp.Models;
using WebApp.ViewModels;

namespace WebApp.Controllers
{
    public static class ControllerExtensions
    {
        public static IActionResult ErrorView(this Controller controller, [CanBeNull] Error[] errors = null)
        {
            return controller.View("Error", new ErrorViewModel
            {
                RequestId = Activity.Current?.Id ?? controller.HttpContext.TraceIdentifier,
                Errors = errors ?? new Error[0]
            });
        }
    }
}