using Microsoft.EntityFrameworkCore;
using InventManagment.Models;

namespace InventManagment.Data
{
    public class InventoryContext : DbContext
    {
        public InventoryContext(DbContextOptions<InventoryContext> options)
            : base(options)
        {
        }

        public DbSet<Item> Items { get; set; } = default!;
    }
}
