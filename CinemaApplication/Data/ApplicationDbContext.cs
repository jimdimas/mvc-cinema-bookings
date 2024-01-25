using CinemaApplication.Models;
using Microsoft.EntityFrameworkCore;
using System.Reflection.Emit;
using System.Reflection.Metadata;

namespace CinemaApplication.Data
{
    public class ApplicationDbContext:DbContext
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : base(options)
        {

        }

        public DbSet<User> Users { get; set; }
        public DbSet<Movie> Movies { get; set; }
        public DbSet<Screening> Screenings { get; set; }
        public DbSet<Cinema> Cinemas { get; set; }
        public DbSet<Booking> Bookings { get; set; }

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
            builder.Entity<Booking>()
                .HasOne(b => b.Screening)
                .WithMany(s => s.Bookings)
                .OnDelete(DeleteBehavior.NoAction);
        }
    }
}
