namespace InventManagment.Models
{
    /// <summary>
    /// This is the model for item categories in the system
    /// Categories help organize inventory items into logical groups
    /// </summary>
    /// <example>
    /// Category category = new Category();
    /// category.Name = "Electronics";
    /// category.Description = "Electronic devices and accessories";
    /// </example>
    public class Category
    {
        /// <summary>
        /// Primary key for the database - this is the unique identifier for each category
        /// </summary>
        public int Id { get; set; }
        
        /// <summary>
        /// Name of the category (like "Electronics", "Office Supplies", "Furniture")
        /// This field is required and helps users identify the category
        /// </summary>
        public string Name { get; set; } = string.Empty;
        
        /// <summary>
        /// Detailed description of what items belong in this category
        /// This helps users understand the category's purpose
        /// </summary>
        public string Description { get; set; } = string.Empty;
        
        /// <summary>
        /// Date and time when the category was created
        /// This is automatically set when a new category is created
        /// </summary>
        public DateTime CreatedAt { get; set; } = DateTime.Now;
        
        /// <summary>
        /// Navigation property for the many-to-many relationship with Items
        /// This allows Entity Framework to manage the relationship
        /// </summary>
        public ICollection<Item> Items { get; set; } = new List<Item>();
    }
} 