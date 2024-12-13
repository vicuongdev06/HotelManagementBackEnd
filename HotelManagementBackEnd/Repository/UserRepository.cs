using HotelManagementBackEnd.Data;
using HotelManagementBackEnd.Interface;
using HotelManagementBackEnd.Models;
using Microsoft.EntityFrameworkCore;

namespace HotelManagementBackEnd.Repository
{
    public class UserRepository : IUserRepository
    {
        private readonly ApplicationDbContext _context;

        public UserRepository(ApplicationDbContext context)
        {
            _context = context;
        }
        public async Task<User> GetUserByUsernameAsync(string username)
        {
            return await _context.Users.FirstOrDefaultAsync(u => u.Username == username);
        }

        public async Task CreateUserAsync(User user)
        {
            await _context.Users.AddAsync(user);
        }

        public async Task SaveChangesAsync()
        {
            await _context.SaveChangesAsync();
        }

		public Task<User?> GetUserByEmailAsync(string email)
		{
			return _context.Users.FirstOrDefaultAsync(u => u.Email == email);
		}

		public async Task<User?> GetUserByIdAsync(int id)
		{
            return await _context.Users.FindAsync(id);
		}
	}
}
