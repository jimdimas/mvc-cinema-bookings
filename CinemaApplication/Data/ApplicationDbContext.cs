using CinemaApplication.Models;
using Microsoft.EntityFrameworkCore;

namespace CinemaApplication.Data
{
    public class ApplicationDbContext:DbContext
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : base(options)
        {

        }

        public DbSet<User> Users { get; set; }
        public DbSet<Movie> Movies { get; set; }

        protected override void OnModelCreating(ModelBuilder builder)
        {
            //Make email attribute of user unique with code first approach
            builder.Entity<User>()
                .HasIndex(user => user.Email)
                .IsUnique();
            builder.Entity<Customer>()
                .ToTable("Customers")
                .HasIndex(c=>c.CustomerId)
                .IsUnique();
            builder.Entity<ContentAdmin>()
                .ToTable("ContentAdmins")
                .HasIndex(c => c.ContentAdminId)
                .IsUnique();
            builder.Entity<Admin>()
                .ToTable("Admins")
                .HasIndex(c => c.AdminId)
                .IsUnique();
        }
    }
}
