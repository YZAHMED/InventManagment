using InventManagment.Data;
using InventManagment.Models;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.IO;
using System.Linq;

namespace InventManagment.Controllers
{
    // Main controller for handling inventory operations
    // This is where all the CRUD stuff happens (Create, Read, Update, Delete)
    public class InventoryController : Controller
    {
        // Database context for talking to the database
        private readonly InventoryContext _context;
        
        // Environment info - needed for file uploads
        private readonly IWebHostEnvironment _env;

        // Constructor - gets dependencies injected
        public InventoryController(InventoryContext context, IWebHostEnvironment env)
        {
            _context = context;
            _env = env;
        }

        // Shows all inventory items on the main page
        public IActionResult Index()
        {
            // Get all items from database and pass to view
            var items = _context.Items.ToList();
            return View(items);
        }

        // Shows the form to create a new item
        public IActionResult Create()
        {
            return View();
        }

        // Handles the form submission when creating a new item
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Create([Bind("Name,Quantity")] Item item, IFormFile? image)
        {
            if (ModelState.IsValid)
            {
                // Handle image upload if user provided one
                if (image != null)
                {
                    // Create uploads folder if it doesn't exist
                    var uploadPath = Path.Combine(_env.WebRootPath, "uploads");
                    Directory.CreateDirectory(uploadPath);
                    
                    // Save the file
                    var filePath = Path.Combine(uploadPath, image.FileName);
                    using var stream = new FileStream(filePath, FileMode.Create);
                    image.CopyTo(stream);
                    
                    // Store the path in the database
                    item.ImagePath = "/uploads/" + image.FileName;
                }

                // Save the new item to database
                _context.Items.Add(item);
                _context.SaveChanges();
                
                // Go back to the list page
                return RedirectToAction(nameof(Index));
            }
            return View(item);
        }

        // Shows the edit form for a specific item
        public IActionResult Edit(int id)
        {
            // Find the item by ID
            var item = _context.Items.Find(id);
            if (item == null)
            {
                return NotFound();
            }
            return View(item);
        }

        // Handles the form submission when editing an item
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Edit(int id, [Bind("Id,Name,Quantity,ImagePath")] Item item, IFormFile? image)
        {
            // Make sure the IDs match
            if (id != item.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    // Handle new image upload if provided
                    if (image != null)
                    {
                        var uploadPath = Path.Combine(_env.WebRootPath, "uploads");
                        Directory.CreateDirectory(uploadPath);
                        var filePath = Path.Combine(uploadPath, image.FileName);
                        using var stream = new FileStream(filePath, FileMode.Create);
                        image.CopyTo(stream);
                        item.ImagePath = "/uploads/" + image.FileName;
                    }

                    // Update the item in database
                    _context.Update(item);
                    _context.SaveChanges();
                }
                catch
                {
                    // If item doesn't exist anymore, show 404
                    if (!ItemExists(item.Id))
                    {
                        return NotFound();
                    }
                    else
                    {
                        // Something else went wrong, re-throw the exception
                        throw;
                    }
                }
                return RedirectToAction(nameof(Index));
            }
            return View(item);
        }

        // Shows the delete confirmation page
        public IActionResult Delete(int id)
        {
            var item = _context.Items.Find(id);
            if (item == null)
            {
                return NotFound();
            }
            return View(item);
        }

        // Actually deletes the item after user confirms
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public IActionResult DeleteConfirmed(int id)
        {
            // Find and remove the item
            var item = _context.Items.Find(id);
            if (item != null)
            {
                _context.Items.Remove(item);
                _context.SaveChanges();
            }
            return RedirectToAction(nameof(Index));
        }

        // Helper method to check if an item exists
        // Used in the edit method to handle errors
        private bool ItemExists(int id)
        {
            return _context.Items.Any(e => e.Id == id);
        }
    }
}
