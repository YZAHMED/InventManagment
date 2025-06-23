namespace InventManagment.Models
{
    // This is the main model for inventory items
    // Each item has an ID, name, quantity, and optional image
    public class Item
    {
        // Primary key for the database
        public int Id { get; set; }
        
        // Name of the inventory item (like "Laptop", "Mouse", etc.)
        public string Name { get; set; } = string.Empty;
        
        // How many of this item we have in stock
        public int Quantity { get; set; }
        
        // Path to the image file (optional - some items might not have pics)
        public string? ImagePath { get; set; }
    }
}
