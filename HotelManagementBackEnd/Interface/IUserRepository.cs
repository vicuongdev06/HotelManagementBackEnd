using HotelManagementBackEnd.Data;

namespace HotelManagementBackEnd.Interface
{
    public interface IUserRepository
    {
        Task<User> GetUserByUsernameAsync(string username);  
        Task CreateUserAsync(User user); 
        Task SaveChangesAsync();  
        Task<User?> GetUserByEmailAsync(string email);
        Task<User?> GetUserByIdAsync(int id);
    }
}
