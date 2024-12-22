using HRSystem.Business;
using HRSystem.Domain.Entities;
using Microsoft.AspNetCore.Mvc;

namespace HRSystem.API.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class UserController : ControllerBase
    {
        private readonly UserService _userService;

        public UserController(UserService userService)
        {
            _userService = userService;
        }

        // POST: api/user/login
        [HttpPost("login")]
        public async Task<IActionResult> Login([FromBody] LoginRequest loginRequest)
        {
            try
            {
                var user = await _userService.LoginAsync(loginRequest.Username, loginRequest.Password);
                return Ok(new { Message = "Giriş başarılı!", User = user });
            }
            catch (Exception ex)
            {
                return Unauthorized(new { Message = ex.Message });
            }
        }

        // POST: api/user/register
        [HttpPost("register")]
        public async Task<IActionResult> Register([FromBody] User user)
        {
            try
            {
                await _userService.AddUserAsync(user);
                return Ok(new { Message = "Kayıt başarılı!", User = user });
            }
            catch (Exception ex)
            {
                return BadRequest(new { Message = ex.Message });
            }
        }
    }

    // Giriş için kullanılan istek modeli
    public class LoginRequest
    {
        public string Username { get; set; }
        public string Password { get; set; }
    }
}
