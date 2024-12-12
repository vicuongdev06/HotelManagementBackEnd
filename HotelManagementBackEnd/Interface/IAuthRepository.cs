using HotelManagementBackEnd.Data;
using HotelManagementBackEnd.Models.Auth;

namespace HotelManagementBackEnd.Interface
{
    public interface IAuthRepository
    {
        Task<string> LoginAsync(string username, string password);
        Task<RegisterResponse> RegisterUserAsync(RegisterRequest request);
        Task<User> GetUserByUsernameAsync(string username);
        Task<User> ResetPasswordAsync(ResetPasswordRequest req);
    }
}
