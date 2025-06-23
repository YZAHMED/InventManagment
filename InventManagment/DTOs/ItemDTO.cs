namespace InventManagment.DTOs
{
    /// <summary>
    /// Data Transfer Object for Item entities
    /// This class is used to transfer item data between the API and client applications
    /// It provides a clean interface without exposing internal implementation details
    /// </summary>
    /// <example>
    /// ItemDTO itemDto = new ItemDTO
    /// {
    ///     Id = 1,
    ///     Name = "Laptop",
    ///     Quantity = 5,
    ///     ImagePath = "/uploads/laptop.jpg",
    ///     CategoryIds = new List<int> { 1, 2 }
    /// };
    /// </example>
    public class ItemDTO
    {
        /// <summary>
        /// Unique identifier for the item
        /// </summary>
        public int Id { get; set; }
        
        /// <summary>
        /// Name of the inventory item
        /// </summary>
        public string Name { get; set; } = string.Empty;
        
        /// <summary>
        /// Current quantity in stock
        /// </summary>
        public int Quantity { get; set; }
        
        /// <summary>
        /// Optional path to the item's image file
        /// </summary>
        public string? ImagePath { get; set; }
        
        /// <summary>
        /// List of category IDs that this item belongs to
        /// This represents the many-to-many relationship with categories
        /// </summary>
        public List<int> CategoryIds { get; set; } = new List<int>();
    }

    /// <summary>
    /// Data Transfer Object for creating new items
    /// This class is used specifically for item creation operations
    /// </summary>
    public class CreateItemDTO
    {
        /// <summary>
        /// Name of the inventory item
        /// </summary>
        public string Name { get; set; } = string.Empty;
        
        /// <summary>
        /// Initial quantity in stock
        /// </summary>
        public int Quantity { get; set; }
        
        /// <summary>
        /// List of category IDs to assign to this item
        /// </summary>
        public List<int> CategoryIds { get; set; } = new List<int>();
    }

    /// <summary>
    /// Data Transfer Object for updating existing items
    /// This class is used specifically for item update operations
    /// </summary>
    public class UpdateItemDTO
    {
        /// <summary>
        /// Name of the inventory item
        /// </summary>
        public string Name { get; set; } = string.Empty;
        
        /// <summary>
        /// Updated quantity in stock
        /// </summary>
        public int Quantity { get; set; }
        
        /// <summary>
        /// Optional path to the item's image file
        /// </summary>
        public string? ImagePath { get; set; }
        
        /// <summary>
        /// List of category IDs to assign to this item
        /// </summary>
        public List<int> CategoryIds { get; set; } = new List<int>();
    }
} 