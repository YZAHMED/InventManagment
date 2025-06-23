using InventManagment.Data;
using InventManagment.DTOs;
using InventManagment.Models;
using Microsoft.EntityFrameworkCore;

namespace InventManagment.Services
{
    /// <summary>
    /// Service implementation for Item operations
    /// This class contains the business logic for all item-related operations
    /// It uses Entity Framework and LINQ to interact with the database
    /// </summary>
    /// <example>
    /// // In Program.cs:
    /// builder.Services.AddScoped<IItemService, ItemService>();
    /// 
    /// // In a controller:
    /// public class ItemController : Controller
    /// {
    ///     private readonly IItemService _itemService;
    ///     public ItemController(IItemService itemService) { _itemService = itemService; }
    /// }
    /// </example>
    public class ItemService : IItemService
    {
        /// <summary>
        /// This is a private variable that will hold the connection to the database
        /// </summary>
        private readonly InventoryContext _context;
        
        /// <summary>
        /// This is a private variable that holds information about the web hosting environment
        /// It is used for file upload operations
        /// </summary>
        private readonly IWebHostEnvironment _env;

        /// <summary>
        /// This is the constructor that will assign the database context and environment
        /// </summary>
        /// <param name="context">The database context for data access</param>
        /// <param name="env">The web hosting environment for file operations</param>
        public ItemService(InventoryContext context, IWebHostEnvironment env)
        {
            // Here the database context is assigned to the private variable
            _context = context;
            // Here the web hosting environment is assigned to the private variable
            _env = env;
        }

        /// <summary>
        /// Gets all items from the database and converts them to DTOs
        /// This method uses LINQ to query the database and includes category information
        /// </summary>
        /// <returns>A list of all items as DTOs with their category information</returns>
        public async Task<IEnumerable<ItemDTO>> GetAllItemsAsync()
        {
            // Use LINQ to query all items with their categories
            var items = await _context.Items
                .Include(i => i.Categories)
                .ToListAsync();

            // Convert to DTOs
            return items.Select(item => new ItemDTO
            {
                Id = item.Id,
                Name = item.Name,
                Quantity = item.Quantity,
                ImagePath = item.ImagePath,
                CategoryIds = item.Categories.Select(c => c.Id).ToList()
            });
        }

        /// <summary>
        /// Gets a specific item by its ID and converts it to a DTO
        /// </summary>
        /// <param name="id">The unique identifier of the item</param>
        /// <returns>The item as a DTO, or null if not found</returns>
        public async Task<ItemDTO?> GetItemByIdAsync(int id)
        {
            // Use LINQ to find the item by ID with its categories
            var item = await _context.Items
                .Include(i => i.Categories)
                .FirstOrDefaultAsync(i => i.Id == id);

            if (item == null) return null;

            // Convert to DTO
            return new ItemDTO
            {
                Id = item.Id,
                Name = item.Name,
                Quantity = item.Quantity,
                ImagePath = item.ImagePath,
                CategoryIds = item.Categories.Select(c => c.Id).ToList()
            };
        }

        /// <summary>
        /// Creates a new item in the database
        /// This method handles image upload and category assignment
        /// </summary>
        /// <param name="createItemDto">The data for creating the new item</param>
        /// <param name="imageFile">Optional image file to upload</param>
        /// <returns>The created item as a DTO</returns>
        public async Task<ItemDTO> CreateItemAsync(CreateItemDTO createItemDto, IFormFile? imageFile = null)
        {
            // Create new item entity
            var item = new Item
            {
                Name = createItemDto.Name,
                Quantity = createItemDto.Quantity
            };

            // Handle image upload if provided
            if (imageFile != null)
            {
                item.ImagePath = await SaveImageAsync(imageFile);
            }

            // Add categories if specified
            if (createItemDto.CategoryIds.Any())
            {
                var categories = await _context.Categories
                    .Where(c => createItemDto.CategoryIds.Contains(c.Id))
                    .ToListAsync();
                item.Categories = categories;
            }

            // Add to database and save
            _context.Items.Add(item);
            await _context.SaveChangesAsync();

            // Return as DTO
            return new ItemDTO
            {
                Id = item.Id,
                Name = item.Name,
                Quantity = item.Quantity,
                ImagePath = item.ImagePath,
                CategoryIds = item.Categories.Select(c => c.Id).ToList()
            };
        }

        /// <summary>
        /// Updates an existing item in the database
        /// This method handles image upload and category updates
        /// </summary>
        /// <param name="id">The unique identifier of the item to update</param>
        /// <param name="updateItemDto">The updated data for the item</param>
        /// <param name="imageFile">Optional new image file to upload</param>
        /// <returns>The updated item as a DTO, or null if not found</returns>
        public async Task<ItemDTO?> UpdateItemAsync(int id, UpdateItemDTO updateItemDto, IFormFile? imageFile = null)
        {
            // Find the existing item
            var item = await _context.Items
                .Include(i => i.Categories)
                .FirstOrDefaultAsync(i => i.Id == id);

            if (item == null) return null;

            // Update properties
            item.Name = updateItemDto.Name;
            item.Quantity = updateItemDto.Quantity;

            // Handle new image upload if provided
            if (imageFile != null)
            {
                item.ImagePath = await SaveImageAsync(imageFile);
            }
            else if (!string.IsNullOrEmpty(updateItemDto.ImagePath))
            {
                item.ImagePath = updateItemDto.ImagePath;
            }

            // Update categories
            if (updateItemDto.CategoryIds.Any())
            {
                var categories = await _context.Categories
                    .Where(c => updateItemDto.CategoryIds.Contains(c.Id))
                    .ToListAsync();
                item.Categories = categories;
            }

            // Save changes
            await _context.SaveChangesAsync();

            // Return as DTO
            return new ItemDTO
            {
                Id = item.Id,
                Name = item.Name,
                Quantity = item.Quantity,
                ImagePath = item.ImagePath,
                CategoryIds = item.Categories.Select(c => c.Id).ToList()
            };
        }

        /// <summary>
        /// Deletes an item from the database
        /// </summary>
        /// <param name="id">The unique identifier of the item to delete</param>
        /// <returns>True if the item was deleted, false if not found</returns>
        public async Task<bool> DeleteItemAsync(int id)
        {
            // Find the item
            var item = await _context.Items.FindAsync(id);
            if (item == null) return false;

            // Remove from database
            _context.Items.Remove(item);
            await _context.SaveChangesAsync();

            return true;
        }

        /// <summary>
        /// Gets items by category using LINQ
        /// </summary>
        /// <param name="categoryId">The unique identifier of the category</param>
        /// <returns>A list of items in the specified category</returns>
        public async Task<IEnumerable<ItemDTO>> GetItemsByCategoryAsync(int categoryId)
        {
            // Use LINQ to find items in the specified category
            var items = await _context.Items
                .Include(i => i.Categories)
                .Where(i => i.Categories.Any(c => c.Id == categoryId))
                .ToListAsync();

            // Convert to DTOs
            return items.Select(item => new ItemDTO
            {
                Id = item.Id,
                Name = item.Name,
                Quantity = item.Quantity,
                ImagePath = item.ImagePath,
                CategoryIds = item.Categories.Select(c => c.Id).ToList()
            });
        }

        /// <summary>
        /// Searches items by name using LINQ
        /// </summary>
        /// <param name="searchTerm">The search term to match against item names</param>
        /// <returns>A list of items matching the search term</returns>
        public async Task<IEnumerable<ItemDTO>> SearchItemsAsync(string searchTerm)
        {
            // Use LINQ to search items by name
            var items = await _context.Items
                .Include(i => i.Categories)
                .Where(i => i.Name.Contains(searchTerm))
                .ToListAsync();

            // Convert to DTOs
            return items.Select(item => new ItemDTO
            {
                Id = item.Id,
                Name = item.Name,
                Quantity = item.Quantity,
                ImagePath = item.ImagePath,
                CategoryIds = item.Categories.Select(c => c.Id).ToList()
            });
        }

        /// <summary>
        /// Helper method to save uploaded images
        /// </summary>
        /// <param name="imageFile">The image file to save</param>
        /// <returns>The path where the image was saved</returns>
        private async Task<string> SaveImageAsync(IFormFile imageFile)
        {
            // Create uploads directory if it doesn't exist
            var uploadPath = Path.Combine(_env.WebRootPath, "uploads");
            Directory.CreateDirectory(uploadPath);

            // Generate unique filename
            var fileName = $"{Guid.NewGuid()}_{imageFile.FileName}";
            var filePath = Path.Combine(uploadPath, fileName);

            // Save the file
            using var stream = new FileStream(filePath, FileMode.Create);
            await imageFile.CopyToAsync(stream);

            return "/uploads/" + fileName;
        }
    }
} 