using Microsoft.EntityFrameworkCore;
using InventManagment.Models;

namespace InventManagment.Data
{
    // This is the database context - it's like a bridge between our code and the database
    // Entity Framework uses this to know what tables exist and how to map them to our models
    public class InventoryContext : DbContext
    {
        // Constructor - gets the connection string and other options
        public InventoryContext(DbContextOptions<InventoryContext> options)
            : base(options)
        {
        }

        // This tells Entity Framework about our Items table
        // DbSet<Item> means "there's a table that stores Item objects"
        public DbSet<Item> Items { get; set; } = default!;
    }
}
