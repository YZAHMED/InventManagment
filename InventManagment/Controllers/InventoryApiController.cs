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
    /// This controller provides API endpoints for inventory management operations
    /// It allows external applications and services to interact with the inventory system
    /// All endpoints return JSON data instead of HTML pages
    /// </summary>
    /// <example>
    /// GET /api/inventoryapi - Returns all inventory items as JSON
    /// POST /api/inventoryapi - Creates a new inventory item
    /// PUT /api/inventoryapi/{id} - Updates an existing item
    /// DELETE /api/inventoryapi/{id} - Removes an item from inventory
    /// </example>
    [Route("api/[controller]")]
    [ApiController]
    public class InventoryApiController : ControllerBase
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
        public InventoryApiController(InventoryContext context, IWebHostEnvironment env)
        {
            // Here the connection to the database is assigned to the private variable from the InventoryContext
            _context = context;
            // Here the web hosting environment is assigned to the private variable
            _env = env;
        }

        /// <summary>
        /// Returns all inventory items as a JSON array
        /// This endpoint can be used by mobile apps or other services to get the complete inventory list
        /// </summary>
        /// <returns>
        /// A JSON array containing all inventory items with their properties
        /// </returns>
        /// <example>
        /// GET /api/inventoryapi
        /// Returns: [{"Id":1,"Name":"Laptop","Quantity":5,"ImagePath":"/uploads/laptop.jpg"},...]
        /// </example>
        [HttpGet]
        public IActionResult GetAll() => Ok(_context.Items.ToList());

        /// <summary>
        /// Returns a specific inventory item by its ID
        /// This endpoint allows retrieving detailed information about a single item
        /// </summary>
        /// <param name="id">The unique identifier of the item to retrieve</param>
        /// <returns>
        /// A JSON object containing the item details, or NotFound if the item doesn't exist
        /// </returns>
        /// <example>
        /// GET /api/inventoryapi/1
        /// Returns: {"Id":1,"Name":"Laptop","Quantity":5,"ImagePath":"/uploads/laptop.jpg"}
        /// </example>
        [HttpGet("{id}")]
        public IActionResult Get(int id)
        {
            // Find the item in the database by its ID
            var item = _context.Items.Find(id);
            return item == null ? NotFound() : Ok(item);
        }

        /// <summary>
        /// Creates a new inventory item in the database
        /// This endpoint accepts form data including an optional image file
        /// </summary>
        /// <param name="item">The item object containing name and quantity information</param>
        /// <param name="image">The optional image file to be uploaded and associated with the item</param>
        /// <returns>
        /// A 201 Created response with the newly created item, including its generated ID
        /// </returns>
        /// <example>
        /// POST /api/inventoryapi
        /// Form data: Name=New Item, Quantity=10, image=file.jpg
        /// Returns: 201 Created with the new item including its ID
        /// </example>
        [HttpPost]
        public IActionResult Create([FromForm] Item item, IFormFile? image)
        {
            // Handle image upload if provided by the client
            if (image != null)
            {
                // Create the uploads directory if it doesn't exist
                var uploadPath = Path.Combine(_env.WebRootPath, "uploads");
                Directory.CreateDirectory(uploadPath);
                
                // Save the uploaded file to the server
                var filePath = Path.Combine(uploadPath, image.FileName);
                using var stream = new FileStream(filePath, FileMode.Create);
                image.CopyTo(stream);
                
                // Store the file path in the database for later retrieval
                item.ImagePath = "/uploads/" + image.FileName;
            }

            // Add the new item to the database and save changes
            _context.Items.Add(item);
            _context.SaveChanges();
            
            // Return 201 Created with the new item and its location
            return CreatedAtAction(nameof(Get), new { id = item.Id }, item);
        }

        /// <summary>
        /// Updates an existing inventory item in the database
        /// This endpoint allows modifying the properties of an existing item
        /// </summary>
        /// <param name="id">The unique identifier of the item to be updated</param>
        /// <param name="updated">The updated item object containing the new values</param>
        /// <param name="image">The optional new image file to replace the existing one</param>
        /// <returns>
        /// A 204 No Content response on successful update, or NotFound if the item doesn't exist
        /// </returns>
        /// <example>
        /// PUT /api/inventoryapi/1
        /// Form data: Name=Updated Item, Quantity=15, image=newfile.jpg
        /// Returns: 204 No Content on success
        /// </example>
        [HttpPut("{id}")]
        public IActionResult Update(int id, [FromForm] Item updated, IFormFile? image)
        {
            // Find the existing item in the database
            var item = _context.Items.Find(id);
            if (item == null) return NotFound();

            // Update the item properties with the new values
            item.Name = updated.Name;
            item.Quantity = updated.Quantity;

            // Handle new image upload if provided
            if (image != null)
            {
                // Create the uploads directory if it doesn't exist
                var uploadPath = Path.Combine(_env.WebRootPath, "uploads");
                Directory.CreateDirectory(uploadPath);
                
                // Save the new image file
                var filePath = Path.Combine(uploadPath, image.FileName);
                using var stream = new FileStream(filePath, FileMode.Create);
                image.CopyTo(stream);
                
                // Update the image path in the database
                item.ImagePath = "/uploads/" + image.FileName;
            }

            // Save the changes to the database
            _context.SaveChanges();
            
            // Return 204 No Content to indicate successful update
            return NoContent();
        }

        /// <summary>
        /// Removes an inventory item from the database permanently
        /// This endpoint deletes the specified item and all its associated data
        /// </summary>
        /// <param name="id">The unique identifier of the item to be deleted</param>
        /// <returns>
        /// A 204 No Content response on successful deletion, or NotFound if the item doesn't exist
        /// </returns>
        /// <example>
        /// DELETE /api/inventoryapi/1
        /// Returns: 204 No Content on successful deletion
        /// </example>
        [HttpDelete("{id}")]
        public IActionResult Delete(int id)
        {
            // Find the item in the database by its ID
            var item = _context.Items.Find(id);
            if (item == null) return NotFound();

            // Remove the item from the database and save changes
            _context.Items.Remove(item);
            _context.SaveChanges();
            
            // Return 204 No Content to indicate successful deletion
            return NoContent();
        }
    }
} 