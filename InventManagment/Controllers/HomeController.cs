using System.Diagnostics;
using InventManagment.Models;
using Microsoft.AspNetCore.Mvc;

namespace InventManagment.Controllers
{
    /// <summary>
    /// This controller handles the main pages of the application including home and privacy
    /// It provides basic navigation and information pages for users
    /// </summary>
    /// <example>
    /// GET /Home/Index - Shows the main homepage
    /// GET /Home/Privacy - Shows the privacy policy page
    /// GET /Home/Error - Shows error information for debugging
    /// </example>
    public class HomeController : Controller
    {
        /// <summary>
        /// This is a private variable that will hold the logger for debugging and tracking application events
        /// </summary>
        private readonly ILogger<HomeController> _logger;

        /// <summary>
        /// This is the constructor that will assign the logger to the private variable
        /// </summary>
        /// <param name="logger">The logger service that provides logging functionality</param>
        public HomeController(ILogger<HomeController> logger)
        {
            // Here the logger is assigned to the private variable for use throughout the controller
            _logger = logger;
        }

        /// <summary>
        /// Shows the main homepage of the application
        /// This method returns a view that displays the welcome page to users
        /// </summary>
        /// <returns>
        /// A view containing the main homepage content
        /// </returns>
        public IActionResult Index()
        {
            // Check if user is authenticated and redirect to inventory if they are
            if (!string.IsNullOrEmpty(HttpContext.Session.GetString("UserId")))
            {
                return RedirectToAction("Index", "Inventory");
            }
            
            // If not authenticated, show the home page
            return View();
        }

        /// <summary>
        /// Shows the privacy policy page
        /// This method returns a view that displays the privacy information to users
        /// </summary>
        /// <returns>
        /// A view containing the privacy policy content
        /// </returns>
        public IActionResult Privacy()
        {
            return View();
        }

        /// <summary>
        /// Shows detailed error information for debugging purposes
        /// This method is used to display error details when something goes wrong in the application
        /// The ResponseCache attribute prevents caching so errors show properly
        /// </summary>
        /// <returns>
        /// A view containing error details including the request ID for debugging
        /// </returns>
        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            // Create error model with request ID for debugging purposes
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}
