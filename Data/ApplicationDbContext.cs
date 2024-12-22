using Microsoft.EntityFrameworkCore;
using api_rest_netcore.Modelos;

namespace api_rest_netcore.Data
{
    public class ApplicationDbContext : DbContext
    {

        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : base(options)
        {
            
        }

        // Pasamos todos los modelos a usar

        public DbSet<User> Users { get; set; }
    }
}
