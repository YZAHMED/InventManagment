using InventManagment.Data;
using InventManagment.Models;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.IO;
using System.Linq;

namespace InventManagment.Controllers
{
    // API controller for mobile apps or other services to use
    // This gives JSON responses instead of HTML pages
    [Route("api/[controller]")]
    [ApiController]
    public class InventoryApiController : ControllerBase
    {
        // Same stuff as the regular controller - database and file handling
        private readonly InventoryContext _context;
        private readonly IWebHostEnvironment _env;

        public InventoryApiController(InventoryContext context, IWebHostEnvironment env)
        {
            _context = context;
            _env = env;
        }

        // GET /api/inventoryapi - returns all items as JSON
        [HttpGet]
        public IActionResult GetAll() => Ok(_context.Items.ToList());

        // GET /api/inventoryapi/{id} - returns one specific item
        [HttpGet("{id}")]
        public IActionResult Get(int id)
        {
            var item = _context.Items.Find(id);
            return item == null ? NotFound() : Ok(item);
        }

        // POST /api/inventoryapi - creates a new item
        [HttpPost]
        public IActionResult Create([FromForm] Item item, IFormFile? image)
        {
            // Handle image upload if provided
            if (image != null)
            {
                var uploadPath = Path.Combine(_env.WebRootPath, "uploads");
                Directory.CreateDirectory(uploadPath);
                var filePath = Path.Combine(uploadPath, image.FileName);
                using var stream = new FileStream(filePath, FileMode.Create);
                image.CopyTo(stream);
                item.ImagePath = "/uploads/" + image.FileName;
            }

            // Save to database
            _context.Items.Add(item);
            _context.SaveChanges();
            
            // Return 201 Created with the new item
            return CreatedAtAction(nameof(Get), new { id = item.Id }, item);
        }

        // PUT /api/inventoryapi/{id} - updates an existing item
        [HttpPut("{id}")]
        public IActionResult Update(int id, [FromForm] Item updated, IFormFile? image)
        {
            var item = _context.Items.Find(id);
            if (item == null) return NotFound();

            // Update the fields
            item.Name = updated.Name;
            item.Quantity = updated.Quantity;

            // Handle new image if provided
            if (image != null)
            {
                var uploadPath = Path.Combine(_env.WebRootPath, "uploads");
                Directory.CreateDirectory(uploadPath);
                var filePath = Path.Combine(uploadPath, image.FileName);
                using var stream = new FileStream(filePath, FileMode.Create);
                image.CopyTo(stream);
                item.ImagePath = "/uploads/" + image.FileName;
            }

            _context.SaveChanges();
            return NoContent(); // 204 No Content - update successful
        }

        // DELETE /api/inventoryapi/{id} - removes an item
        [HttpDelete("{id}")]
        public IActionResult Delete(int id)
        {
            var item = _context.Items.Find(id);
            if (item == null) return NotFound();

            _context.Items.Remove(item);
            _context.SaveChanges();
            return NoContent(); // 204 No Content - delete successful
        }
    }
} 