using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Newtonsoft.Json.Linq;
using SalesManagementSystem.DAL.Repositories;
using SalesManagementSystem.DATA.Entites;
using SalesManagementSystem.SERVICE.DTOs.UserModels;
using SalesManagementSystem.SERVICE.Helpers;
using SalesManagementSystem.SERVICE.Interfaces;
using System.Security.Claims;

namespace SalesManagementSystem.API.Controllers
{

    [Authorize] // Only authenticated users can access
    [Route("api/[controller]")]
    [ApiController]
    public class UserController : ControllerBase
    {
        private readonly IUserService _userService;
        //private readonly IAuthService _authService;
        private readonly IConfiguration _configuration;
        private readonly IPasswordHasher _passwordHasher;
        public UserController(IPasswordHasher passwordHasher, IConfiguration configuration, IUserService userService)
        {
            _userService = userService;
            //_authService = authService;
            _configuration = configuration;
            _passwordHasher = passwordHasher;
        }

        [AllowAnonymous]
        [HttpPost("Register")]
        public async Task<IActionResult> RegisterUserAsync([FromBody] RegisterUserModel model)
        {
            if (ModelState.IsValid)
            {
                var result = await _userService.RegisterUserWithPersonAsync(model);
                if (!result.Success)
                    return BadRequest(result.Message);

                return Ok(result);
            }

            return BadRequest();
        }

        [AllowAnonymous]
        [HttpPut("Login")]
        public async Task<IActionResult> Login([FromBody] LoginModel model)
        {
            var result = await _userService.LoginUserAsync(model);
            if (result.Success)
            {
                var loggedUser = await _userService.GetUserByEmailAsync(model.UserName);
                var user = await _userService.GetUserWithRolesByIdAsync(loggedUser.UserId);
                var token = TokenHelper.TokenGeneration(user, _configuration);
                HttpContext.Response.Cookies.Append("Token", token);

                return Ok(new { Token = token, result.Message });
            }

            return Unauthorized(result.Message);
        }

        [HttpGet("LoginWithRole")]
        public IActionResult LoginWithRole([FromQuery] string role, [FromBody] LoginModel model)
        {
            if (role.ToLower() == "admin")
                return Ok("Welcome, Admin!");
            if (role.ToLower() == "user")
                return Ok("Welcome, User!");

            return Forbid(); // 403 Forbidden if role is unknown
        }

        [Authorize(Roles = "User,Admin")] // Only Users and Admins
        [HttpPut("Update/{id}")]
        public async Task<IActionResult> UpdateUser(int id, [FromBody] UpdateUserModel model)
        {
            var userId = int.Parse(User.FindFirst(ClaimTypes.NameIdentifier)?.Value);
            var result = await _userService.UpdateUserAsync(userId, model);
            if (!result.Success)
                return BadRequest(result.Message);

            return Ok(result);
        }


        [HttpGet("debug-claims")]
        public IActionResult DebugClaims()
        {
            var claims = User.Claims.Select(c => new { c.Type, c.Value }).ToList();
            return Ok(claims);
        }

        [AllowAnonymous] // Require authentication
        [HttpGet("Profile")]
        public IActionResult GetProfile()
        {
            var userId = User.Claims.FirstOrDefault(c => c.Type == ClaimTypes.NameIdentifier)?.Value;
            var userName = User.Claims.FirstOrDefault(c => c.Type == ClaimTypes.Name)?.Value;

            var roles = User.Claims
                        .Where(c => c.Type == ClaimTypes.Role) // ✅ Get only Role claims
                        .Select(c => c.Value) // ✅ Extract role names
                        .ToList(); // ✅ Convert to List<string>

            if (userId == null)
                return Unauthorized("Invalid token or user not found");

            return Ok(new
            {
                UserId = userId,
                UserName = userName,
                Roles = roles
            });
        }

        [HttpPut("LogOut")]
        public async Task<ActionResult> LogOut()
        {
            HttpContext.Response.Cookies.Delete("Token");

            return Ok("logged out!");
        }

        [HttpGet("generate-hashed-password")]
        public async Task<ActionResult<string>> GenerateHashed(string password)
        {
            return await _passwordHasher.HashPassword(password);
        }
    }
}
