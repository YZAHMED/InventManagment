using System.Diagnostics;
using InventManagment.Models;
using Microsoft.AspNetCore.Mvc;

namespace InventManagment.Controllers
{
    // This controller handles the main pages like home and privacy
    // Pretty basic stuff - just shows views mostly
    public class HomeController : Controller
    {
        // Logger for debugging and keeping track of what's happening
        private readonly ILogger<HomeController> _logger;

        // Constructor - gets the logger injected automatically
        public HomeController(ILogger<HomeController> logger)
        {
            _logger = logger;
        }

        // Shows the main homepage
        public IActionResult Index()
        {
            return View();
        }

        // Privacy policy page - probably not used much but good to have
        public IActionResult Privacy()
        {
            return View();
        }

        // Error page - shows when something goes wrong
        // The ResponseCache stuff prevents caching so errors show properly
        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            // Create error model with request ID for debugging
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}
