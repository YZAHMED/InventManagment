using InventManagment.Data;
using InventManagment.Models;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.IO;
using System.Linq;

namespace InventManagment.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class InventoryApiController : ControllerBase
    {
        private readonly InventoryContext _context;
        private readonly IWebHostEnvironment _env;

        public InventoryApiController(InventoryContext context, IWebHostEnvironment env)
        {
            _context = context;
            _env = env;
        }

        [HttpGet]
        public IActionResult GetAll() => Ok(_context.Items.ToList());

        [HttpGet("{id}")]
        public IActionResult Get(int id)
        {
            var item = _context.Items.Find(id);
            return item == null ? NotFound() : Ok(item);
        }

        [HttpPost]
        public IActionResult Create([FromForm] Item item, IFormFile? image)
        {
            if (image != null)
            {
                var uploadPath = Path.Combine(_env.WebRootPath, "uploads");
                Directory.CreateDirectory(uploadPath);
                var filePath = Path.Combine(uploadPath, image.FileName);
                using var stream = new FileStream(filePath, FileMode.Create);
                image.CopyTo(stream);
                item.ImagePath = "/uploads/" + image.FileName;
            }

            _context.Items.Add(item);
            _context.SaveChanges();
            return CreatedAtAction(nameof(Get), new { id = item.Id }, item);
        }

        [HttpPut("{id}")]
        public IActionResult Update(int id, [FromForm] Item updated, IFormFile? image)
        {
            var item = _context.Items.Find(id);
            if (item == null) return NotFound();

            item.Name = updated.Name;
            item.Quantity = updated.Quantity;

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
            return NoContent();
        }

        [HttpDelete("{id}")]
        public IActionResult Delete(int id)
        {
            var item = _context.Items.Find(id);
            if (item == null) return NotFound();

            _context.Items.Remove(item);
            _context.SaveChanges();
            return NoContent();
        }
    }
} 