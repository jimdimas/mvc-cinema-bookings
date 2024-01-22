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

        protected override void OnModelCreating(ModelBuilder builder)
        {
            //Make email attribute of user unique with code first approach
            builder.Entity<User>()
                .HasIndex(user => user.Email)
                .IsUnique();
        }
    }
}
