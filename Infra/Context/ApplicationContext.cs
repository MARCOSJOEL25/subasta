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
    }
}
