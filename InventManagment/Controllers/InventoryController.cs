using InventManagment.Data;
using InventManagment.Models;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.IO;
using System.Linq;

namespace InventManagment.Controllers
{
    /// <summary>
    /// This controller handles all inventory management operations including CRUD operations
    /// It provides endpoints for viewing, creating, editing, and deleting inventory items
    /// All operations require user authentication to access
    /// </summary>
    /// <example>
    /// GET /Inventory/Index - Shows all inventory items
    /// POST /Inventory/Create - Creates a new inventory item
    /// PUT /Inventory/Edit/1 - Updates an existing item
    /// DELETE /Inventory/Delete/1 - Removes an item from inventory
    /// </example>
    public class InventoryController : Controller
    {
        /// <summary>
        /// This is a private variable that will hold the connection to the database after being assigned in the constructor
        /// </summary>
        private readonly InventoryContext _context;
        
        /// <summary>
        /// This is a private variable that holds information about the web hosting environment
        /// It is used for file upload operations to determine where to store uploaded images
        /// </summary>
        private readonly IWebHostEnvironment _env;

        /// <summary>
        /// This is the constructor that will assign the connection to the database to the private variable
        /// </summary>
        /// <param name="context">The database context that provides access to the Items table</param>
        /// <param name="env">The web hosting environment information for file operations</param>
        public InventoryController(InventoryContext context, IWebHostEnvironment env)
        {
            // Here the connection to the database is assigned to the private variable from the InventoryContext
            _context = context;
            // Here the web hosting environment is assigned to the private variable
            _env = env;
        }

        /// <summary>
        /// Shows all inventory items in a table format
        /// This method retrieves all items from the database and displays them to the user
        /// </summary>
        /// <returns>
        /// A view containing a table of all inventory items with their details
        /// </returns>
        public IActionResult Index()
        {
            // Check if user is authenticated before allowing access to inventory
            if (string.IsNullOrEmpty(HttpContext.Session.GetString("UserId")))
            {
                return RedirectToAction("Login", "Auth");
            }

            // Get all items from database and pass to view
            var items = _context.Items.ToList();
            return View(items);
        }

        /// <summary>
        /// Shows the form to create a new inventory item
        /// This method returns a view that displays the item creation form to the user
        /// </summary>
        /// <returns>
        /// A view containing a form with fields for item name, quantity, and optional image upload
        /// </returns>
        public IActionResult Create()
        {
            // Check if user is authenticated before allowing access to create form
            if (string.IsNullOrEmpty(HttpContext.Session.GetString("UserId")))
            {
                return RedirectToAction("Login", "Auth");
            }

            return View();
        }

        /// <summary>
        /// Handles the form submission when creating a new inventory item
        /// This method processes the form data, handles image uploads, and saves the new item to the database
        /// </summary>
        /// <param name="item">The item object containing the name and quantity from the form</param>
        /// <param name="image">The optional image file uploaded by the user</param>
        /// <returns>
        /// Redirects to the inventory list on successful creation, or returns to form with validation errors
        /// </returns>
        /// <example>
        /// POST /Inventory/Create
        /// Form data: Name=Laptop, Quantity=5, image=file.jpg
        /// </example>
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Create([Bind("Name,Quantity")] Item item, IFormFile? image)
        {
            // Check if user is authenticated before allowing item creation
            if (string.IsNullOrEmpty(HttpContext.Session.GetString("UserId")))
            {
                return RedirectToAction("Login", "Auth");
            }

            if (ModelState.IsValid)
            {
                // Handle image upload if user provided one
                if (image != null)
                {
                    // Create uploads folder if it doesn't exist
                    var uploadPath = Path.Combine(_env.WebRootPath, "uploads");
                    Directory.CreateDirectory(uploadPath);
                    
                    // Save the file to the uploads directory
                    var filePath = Path.Combine(uploadPath, image.FileName);
                    using var stream = new FileStream(filePath, FileMode.Create);
                    image.CopyTo(stream);
                    
                    // Store the path in the database for later retrieval
                    item.ImagePath = "/uploads/" + image.FileName;
                }

                // Save the new item to database
                _context.Items.Add(item);
                _context.SaveChanges();
                
                // Go back to the list page after successful creation
                return RedirectToAction(nameof(Index));
            }
            return View(item);
        }

        /// <summary>
        /// Shows the edit form for a specific inventory item
        /// This method retrieves the item by ID and displays a form pre-filled with current values
        /// </summary>
        /// <param name="id">The unique identifier of the item to be edited</param>
        /// <returns>
        /// A view containing a form with the current item values, or NotFound if item doesn't exist
        /// </returns>
        public IActionResult Edit(int id)
        {
            // Check if user is authenticated before allowing access to edit form
            if (string.IsNullOrEmpty(HttpContext.Session.GetString("UserId")))
            {
                return RedirectToAction("Login", "Auth");
            }

            // Find the item by ID in the database
            var item = _context.Items.Find(id);
            if (item == null)
            {
                return NotFound();
            }
            return View(item);
        }

        /// <summary>
        /// Handles the form submission when editing an existing inventory item
        /// This method processes the form data, handles new image uploads, and updates the item in the database
        /// </summary>
        /// <param name="id">The unique identifier of the item being edited</param>
        /// <param name="item">The updated item object containing the new values</param>
        /// <param name="image">The optional new image file uploaded by the user</param>
        /// <returns>
        /// Redirects to the inventory list on successful update, or returns to form with validation errors
        /// </returns>
        /// <example>
        /// POST /Inventory/Edit/1
        /// Form data: Id=1, Name=Updated Laptop, Quantity=10, image=newfile.jpg
        /// </example>
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Edit(int id, [Bind("Id,Name,Quantity,ImagePath")] Item item, IFormFile? image)
        {
            // Check if user is authenticated before allowing item updates
            if (string.IsNullOrEmpty(HttpContext.Session.GetString("UserId")))
            {
                return RedirectToAction("Login", "Auth");
            }

            // Make sure the IDs match to prevent data corruption
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
                    // If item doesn't exist anymore, show 404 error
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

        /// <summary>
        /// Shows the delete confirmation page for a specific inventory item
        /// This method retrieves the item by ID and displays a confirmation page before deletion
        /// </summary>
        /// <param name="id">The unique identifier of the item to be deleted</param>
        /// <returns>
        /// A view containing the item details and a confirmation form, or NotFound if item doesn't exist
        /// </returns>
        public IActionResult Delete(int id)
        {
            // Check if user is authenticated before allowing access to delete confirmation
            if (string.IsNullOrEmpty(HttpContext.Session.GetString("UserId")))
            {
                return RedirectToAction("Login", "Auth");
            }

            var item = _context.Items.Find(id);
            if (item == null)
            {
                return NotFound();
            }
            return View(item);
        }

        /// <summary>
        /// Actually deletes the item from the database after user confirms the deletion
        /// This method removes the specified item from the database permanently
        /// </summary>
        /// <param name="id">The unique identifier of the item to be deleted</param>
        /// <returns>
        /// Redirects to the inventory list after successful deletion
        /// </returns>
        /// <example>
        /// POST /Inventory/Delete/1
        /// Confirms deletion of item with ID 1
        /// </example>
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public IActionResult DeleteConfirmed(int id)
        {
            // Check if user is authenticated before allowing item deletion
            if (string.IsNullOrEmpty(HttpContext.Session.GetString("UserId")))
            {
                return RedirectToAction("Login", "Auth");
            }

            // Find and remove the item from the database
            var item = _context.Items.Find(id);
            if (item != null)
            {
                _context.Items.Remove(item);
                _context.SaveChanges();
            }
            return RedirectToAction(nameof(Index));
        }

        /// <summary>
        /// Helper method to check if an item exists in the database
        /// This method is used in the edit method to handle errors when an item doesn't exist
        /// </summary>
        /// <param name="id">The unique identifier of the item to check</param>
        /// <returns>
        /// True if the item exists in the database, false otherwise
        /// </returns>
        /// <example>
        /// bool exists = ItemExists(5);
        /// // Returns: true if item with ID 5 exists, false otherwise
        /// </example>
        private bool ItemExists(int id)
        {
            return _context.Items.Any(e => e.Id == id);
        }
    }
}
