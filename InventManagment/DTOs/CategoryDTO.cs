namespace InventManagment.DTOs
{
    /// <summary>
    /// Data Transfer Object for Category entities
    /// This class is used to transfer category data between the API and client applications
    /// It provides a clean interface without exposing internal implementation details
    /// </summary>
    /// <example>
    /// CategoryDTO categoryDto = new CategoryDTO
    /// {
    ///     Id = 1,
    ///     Name = "Electronics",
    ///     Description = "Electronic devices and accessories",
    ///     ItemCount = 5
    /// };
    /// </example>
    public class CategoryDTO
    {
        /// <summary>
        /// Unique identifier for the category
        /// </summary>
        public int Id { get; set; }
        
        /// <summary>
        /// Name of the category
        /// </summary>
        public string Name { get; set; } = string.Empty;
        
        /// <summary>
        /// Detailed description of the category
        /// </summary>
        public string Description { get; set; } = string.Empty;
        
        /// <summary>
        /// Date and time when the category was created
        /// </summary>
        public DateTime CreatedAt { get; set; }
        
        /// <summary>
        /// Number of items currently in this category
        /// This is calculated for display purposes
        /// </summary>
        public int ItemCount { get; set; }
    }

    /// <summary>
    /// Data Transfer Object for creating new categories
    /// This class is used specifically for category creation operations
    /// </summary>
    public class CreateCategoryDTO
    {
        /// <summary>
        /// Name of the category
        /// </summary>
        public string Name { get; set; } = string.Empty;
        
        /// <summary>
        /// Detailed description of the category
        /// </summary>
        public string Description { get; set; } = string.Empty;
    }

    /// <summary>
    /// Data Transfer Object for updating existing categories
    /// This class is used specifically for category update operations
    /// </summary>
    public class UpdateCategoryDTO
    {
        /// <summary>
        /// Name of the category
        /// </summary>
        public string Name { get; set; } = string.Empty;
        
        /// <summary>
        /// Detailed description of the category
        /// </summary>
        public string Description { get; set; } = string.Empty;
    }
} 