using HotelManagementBackEnd.Data;
using HotelManagementBackEnd.Models;
using Microsoft.EntityFrameworkCore;

namespace HotelManagementBackEnd.Data
{
    public class ApplicationDbContext : DbContext
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : base(options) { }

        public DbSet<User> Users { get; set; }  
    }
}
