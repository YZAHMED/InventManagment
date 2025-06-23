using InventManagment.Data;
using InventManagment.Models;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.IO;
using System.Linq;

namespace InventManagment.Controllers
{
    public class InventoryController : Controller
    {
        private readonly InventoryContext _context;
        private readonly IWebHostEnvironment _env;

        public InventoryController(InventoryContext context, IWebHostEnvironment env)
        {
            _context = context;
            _env = env;
        }

        // MVC Actions for Web Interface
        public IActionResult Index()
        {
            var items = _context.Items.ToList();
            return View(items);
        }

        public IActionResult Create()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Create([Bind("Name,Quantity")] Item item, IFormFile? image)
        {
            if (ModelState.IsValid)
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
                return RedirectToAction(nameof(Index));
            }
            return View(item);
        }

        public IActionResult Edit(int id)
        {
            var item = _context.Items.Find(id);
            if (item == null)
            {
                return NotFound();
            }
            return View(item);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Edit(int id, [Bind("Id,Name,Quantity,ImagePath")] Item item, IFormFile? image)
        {
            if (id != item.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
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

                    _context.Update(item);
                    _context.SaveChanges();
                }
                catch
                {
                    if (!ItemExists(item.Id))
                    {
                        return NotFound();
                    }
                    else
                    {
                        throw;
                    }
                }
                return RedirectToAction(nameof(Index));
            }
            return View(item);
        }

        public IActionResult Delete(int id)
        {
            var item = _context.Items.Find(id);
            if (item == null)
            {
                return NotFound();
            }
            return View(item);
        }

        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public IActionResult DeleteConfirmed(int id)
        {
            var item = _context.Items.Find(id);
            if (item != null)
            {
                _context.Items.Remove(item);
                _context.SaveChanges();
            }
            return RedirectToAction(nameof(Index));
        }

        private bool ItemExists(int id)
        {
            return _context.Items.Any(e => e.Id == id);
        }
    }
}
