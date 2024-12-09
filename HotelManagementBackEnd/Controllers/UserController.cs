using HotelManagementBackEnd.Models;
using HotelManagementBackEnd.Interface;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using HotelManagementBackEnd.Data;

namespace HotelManagementBackEnd.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [Authorize] 
    public class UserController : ControllerBase
    {
        private readonly IUserRepository _userRepository;

        public UserController(IUserRepository userRepository)
        {
            _userRepository = userRepository;
        }

        [HttpGet("profile")]
        public async Task<IActionResult> GetProfile()
        {
            var userId = int.Parse(User.Identity.Name); 
            var user = await _userRepository.GetUserByUsernameAsync(userId.ToString()); 

            if (user == null)
            {
                return NotFound(new { message = "User not found" });
            }

            return Ok(user);
        }

        [HttpPut("update")]
        public async Task<IActionResult> UpdateProfile([FromBody] User updateUser)
        {
            var userId = int.Parse(User.Identity.Name);
            var user = await _userRepository.GetUserByUsernameAsync(userId.ToString());

            if (user == null)
            {
                return NotFound(new { message = "User not found" });
            }

            user.FirstName = updateUser.FirstName;
            user.LastName = updateUser.LastName;
            user.Email = updateUser.Email;
            user.PhoneNumber = updateUser.PhoneNumber;
            user.Address = updateUser.Address;
            user.AvatarUrl = updateUser.AvatarUrl;
            user.UpdatedAt = DateTime.UtcNow;

            await _userRepository.SaveChangesAsync();

            return Ok(new { message = "Profile updated successfully" });
        }
    }
}
