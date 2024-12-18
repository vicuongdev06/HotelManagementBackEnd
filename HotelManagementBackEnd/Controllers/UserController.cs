using HotelManagementBackEnd.Models;
using HotelManagementBackEnd.Interface;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using HotelManagementBackEnd.Data;
using System.Security.Claims;
using HotelManagementBackEnd.Models.User;

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
        public async Task<ActionResult<UserResponse>> GetProfile()
        {
            var userId = User.FindFirst(ClaimTypes.NameIdentifier)?.Value; 
            var user = await _userRepository.GetUserByIdAsync(int.Parse(userId)); 

            if (user == null)
            {
                return Unauthorized(new { message = "Không tìm thấy tài khoản" });
            }

            return Ok(user);
        }

        [HttpPut("update")]
        public async Task<IActionResult> UpdateProfile([FromBody] User updateUser)
        {
			var userId = User.FindFirst(ClaimTypes.NameIdentifier)?.Value;
			var user = await _userRepository.GetUserByIdAsync(int.Parse(userId));

			if (user == null)
            {
                return Unauthorized(new { message = "Không tìm thấy tài khoản" });
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
