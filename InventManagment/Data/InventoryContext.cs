using Microsoft.EntityFrameworkCore;
using InventManagment.Models;

namespace InventManagment.Data
{
    /// <summary>
    /// This is the database context class that acts as a bridge between our code and the database
    /// Entity Framework uses this class to know what tables exist and how to map them to our models
    /// </summary>
    /// <example>
    /// InventoryContext context = new InventoryContext(options);
    /// var items = context.Items.ToList();
    /// var users = context.Users.ToList();
    /// var categories = context.Categories.ToList();
    /// </example>
    public class InventoryContext : DbContext
    {
        /// <summary>
        /// Constructor that receives the database connection options
        /// These options include the connection string and other database configuration settings
        /// </summary>
        /// <param name="options">Database configuration options including connection string</param>
        public InventoryContext(DbContextOptions<InventoryContext> options)
            : base(options)
        {
        }

        /// <summary>
        /// This DbSet represents the Items table in the database
        /// DbSet<Item> tells Entity Framework that there's a table that stores Item objects
        /// This allows us to perform CRUD operations on the Items table
        /// </summary>
        public DbSet<Item> Items { get; set; } = default!;
        
        /// <summary>
        /// This DbSet represents the Users table in the database
        /// DbSet<User> tells Entity Framework that there's a table that stores User objects
        /// This allows us to perform CRUD operations on the Users table for authentication
        /// </summary>
        public DbSet<User> Users { get; set; } = default!;
        
        /// <summary>
        /// This DbSet represents the Categories table in the database
        /// DbSet<Category> tells Entity Framework that there's a table that stores Category objects
        /// This allows us to perform CRUD operations on the Categories table
        /// </summary>
        public DbSet<Category> Categories { get; set; } = default!;

        /// <summary>
        /// This method is called when the model is being created
        /// It allows us to configure the many-to-many relationship between Items and Categories
        /// </summary>
        /// <param name="modelBuilder">The model builder used to configure the database model</param>
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            // Configure the many-to-many relationship between Items and Categories
            modelBuilder.Entity<Item>()
                .HasMany(i => i.Categories)
                .WithMany(c => c.Items)
                .UsingEntity(j => j.ToTable("ItemCategories"));
        }
    }
}
