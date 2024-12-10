using HotelManagementBackEnd.Models.Auth;
using HotelManagementBackEnd.Interface;
using Microsoft.AspNetCore.Mvc;
using Microsoft.IdentityModel.Tokens;
using HotelManagementBackEnd.Constants;

namespace HotelManagementBackEnd.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AuthController : ControllerBase
    {
        private readonly IAuthRepository _authRepository;

        public AuthController(IAuthRepository authRepository)
        {
            _authRepository = authRepository;
        }

        [HttpPost("login")]
        public async Task<IActionResult> Login([FromBody] LoginRequest loginRequest)
        {
            var token = await _authRepository.LoginAsync(loginRequest.Username, loginRequest.Password);

            if (string.IsNullOrEmpty(token))
            {
                return Unauthorized(new { message = "Invalid credentials" });
            }

            var user = await _authRepository.GetUserByUsernameAsync(loginRequest.Username);

            if (user == null)
            {
                return Unauthorized(new { message = "User not found" });
            }

            var userInfo = new
            {
                user.Id,
                user.Username,
                user.Email,
                user.FirstName,
                user.LastName,
                user.Role
            };

            return Ok(new
            {
                Message = "Login successful",
                Token = token,
                User = userInfo
            });
        }


        [HttpPost("register")]
        public async Task<IActionResult> Register([FromBody] RegisterRequest registerRequest)
        {
            var response = await _authRepository.RegisterUserAsync(registerRequest);

            if (!response.Success)
            {
                return BadRequest(new { message = response.Message });
            }

            var user = await _authRepository.GetUserByUsernameAsync(registerRequest.Username);

            if (user == null)
            {
                return BadRequest(new { message = "Error occurred while fetching user data." });
            }

            return Ok(new
            {
                Message = response.Message,
                User = new
                {
                    user.Id, 
                    user.Username,
                    user.Email,
                    user.FirstName,
                    user.LastName,
                    user.Role
                }
            });
        }

    }
}
