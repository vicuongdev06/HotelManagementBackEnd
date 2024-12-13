using HotelManagementBackEnd.Data;
using HotelManagementBackEnd.Models;
using Microsoft.EntityFrameworkCore;

namespace HotelManagementBackEnd.Data
{
    public class ApplicationDbContext : DbContext
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : base(options) { }

        public DbSet<User> Users { get; set; }
        public DbSet<Room> Rooms { get; set; }
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);
            modelBuilder.Entity<Room>(entity =>
            {
                entity.Property(e => e.IsAvailable).HasDefaultValue(true);
                entity.Property(e => e.CreatedAt).HasDefaultValueSql("GETDATE()");
            });
        }
    }
}

