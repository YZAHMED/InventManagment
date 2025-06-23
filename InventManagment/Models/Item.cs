namespace InventManagment.Models
{
    /// <summary>
    /// This is the main model for inventory items in the system
    /// Each item has an ID, name, quantity, optional image, and can belong to multiple categories
    /// </summary>
    /// <example>
    /// Item item = new Item();
    /// item.Name = "Laptop";
    /// item.Quantity = 5;
    /// </example>
    public class Item
    {
        /// <summary>
        /// Primary key for the database - this is the unique identifier for each item
        /// </summary>
        public int Id { get; set; }
        
        /// <summary>
        /// Name of the inventory item (like "Laptop", "Mouse", "Keyboard", etc.)
        /// This field cannot be empty and must have a value
        /// </summary>
        public string Name { get; set; } = string.Empty;
        
        /// <summary>
        /// How many of this item we have in stock
        /// This represents the current quantity available in the inventory
        /// </summary>
        public int Quantity { get; set; }
        
        /// <summary>
        /// Path to the image file stored on the server (optional - some items might not have pictures)
        /// This is used to display product images in the web interface
        /// </summary>
        public string? ImagePath { get; set; }
        
        /// <summary>
        /// Navigation property for the many-to-many relationship with Categories
        /// This allows Entity Framework to manage the relationship
        /// </summary>
        public ICollection<Category> Categories { get; set; } = new List<Category>();
    }
}
