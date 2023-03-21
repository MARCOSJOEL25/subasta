using core.Models;
using Microsoft.EntityFrameworkCore;

namespace subasta.Context
{
    public class ApplicationContext : DbContext
    {
        public ApplicationContext(DbContextOptions<ApplicationContext> options): base(options) 
        { 
        
        }

        public DbSet<user> user { get; set; }
        public DbSet<Product> Product { get; set; }

        public DbSet<Category> category { get; set; }
    }
}
