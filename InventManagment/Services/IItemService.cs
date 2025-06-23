using InventManagment.DTOs;
using InventManagment.Models;

namespace InventManagment.Services
{
    /// <summary>
    /// Service interface for Item operations
    /// This interface defines the contract for all item-related business logic
    /// It allows for dependency injection and easier testing
    /// </summary>
    /// <example>
    /// // In Startup.cs or Program.cs:
    /// services.AddScoped<IItemService, ItemService>();
    /// 
    /// // In a controller:
    /// public class ItemController : Controller
    /// {
    ///     private readonly IItemService _itemService;
    ///     public ItemController(IItemService itemService) { _itemService = itemService; }
    /// }
    /// </example>
    public interface IItemService
    {
        /// <summary>
        /// Gets all items from the database
        /// </summary>
        /// <returns>A list of all items as DTOs</returns>
        Task<IEnumerable<ItemDTO>> GetAllItemsAsync();

        /// <summary>
        /// Gets a specific item by its ID
        /// </summary>
        /// <param name="id">The unique identifier of the item</param>
        /// <returns>The item as a DTO, or null if not found</returns>
        Task<ItemDTO?> GetItemByIdAsync(int id);

        /// <summary>
        /// Creates a new item in the database
        /// </summary>
        /// <param name="createItemDto">The data for creating the new item</param>
        /// <param name="imageFile">Optional image file to upload</param>
        /// <returns>The created item as a DTO</returns>
        Task<ItemDTO> CreateItemAsync(CreateItemDTO createItemDto, IFormFile? imageFile = null);

        /// <summary>
        /// Updates an existing item in the database
        /// </summary>
        /// <param name="id">The unique identifier of the item to update</param>
        /// <param name="updateItemDto">The updated data for the item</param>
        /// <param name="imageFile">Optional new image file to upload</param>
        /// <returns>The updated item as a DTO, or null if not found</returns>
        Task<ItemDTO?> UpdateItemAsync(int id, UpdateItemDTO updateItemDto, IFormFile? imageFile = null);

        /// <summary>
        /// Deletes an item from the database
        /// </summary>
        /// <param name="id">The unique identifier of the item to delete</param>
        /// <returns>True if the item was deleted, false if not found</returns>
        Task<bool> DeleteItemAsync(int id);

        /// <summary>
        /// Gets items by category
        /// </summary>
        /// <param name="categoryId">The unique identifier of the category</param>
        /// <returns>A list of items in the specified category</returns>
        Task<IEnumerable<ItemDTO>> GetItemsByCategoryAsync(int categoryId);

        /// <summary>
        /// Searches items by name
        /// </summary>
        /// <param name="searchTerm">The search term to match against item names</param>
        /// <returns>A list of items matching the search term</returns>
        Task<IEnumerable<ItemDTO>> SearchItemsAsync(string searchTerm);
    }
} 