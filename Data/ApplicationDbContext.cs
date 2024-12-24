using Microsoft.EntityFrameworkCore;
using api_rest_netcore.Models;
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
        public DbSet<Contact> Contacts { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            modelBuilder.Entity<User>().ToTable("Users", "colegio");
            modelBuilder.Entity<Contact>().ToTable("Contacts", "colegio");
        }
    }
}
