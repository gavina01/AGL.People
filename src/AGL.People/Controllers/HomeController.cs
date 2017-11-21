namespace AGL_People.Controllers
{
    using Microsoft.AspNetCore.Mvc;
    using System.Diagnostics;

    /// <summary>
    /// Home Controller for SPA Landing
    /// </summary>
    public class HomeController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }

        public IActionResult Error()
        {
            ViewData["RequestId"] = Activity.Current?.Id ?? HttpContext.TraceIdentifier;
            return View();
        }
    }
}