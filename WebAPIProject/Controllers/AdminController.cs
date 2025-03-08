using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using SalesManagementSystem.SERVICE.DTOs.UserModels;
using SalesManagementSystem.SERVICE.Interfaces;

namespace SalesManagementSystem.API.Controllers
{
    [Authorize(Roles = "Admin")] // Protect entire controller
    [Route("api/[controller]")]
    [ApiController]
    //[Authorize(Roles = "Admin")] // ✅ All actions require admin access
    public class AdminController : ControllerBase
    {
        private readonly IUserService _userService;

        public AdminController(IUserService userService)
        {
            _userService = userService;
        }

        [HttpGet("GetAllUsersWithPersons")]
        public async Task<ActionResult<IEnumerable<UserModel>>> GetAllUsersWithPersons()
        {
            var users = await _userService.GetAllUsersWithPersonAsync();
            if (users == null)
                return NotFound();

            return Ok(users);
        }


        [HttpGet("GetUserWithPerson/{id}")]
        public async Task<ActionResult<UserModel>> GetUserWithPersonById(int id)
        {
            var user = await _userService.GetUserWithPersonByIdAsync(id);
            if (user == null)
                return NotFound();

            return Ok(user);
        }


        [HttpPost("assign-roles")]
        public async Task<IActionResult> AssignUserRoles([FromQuery] int userId, [FromBody] List<int> roleIds)
        {
            var result = await _userService.AssignRoleAsync(userId, roleIds);
            if (!result.Success)
                return BadRequest(result.Message);

            return Ok(result);
        }

        [HttpDelete("Delete/{id}")]
        public async Task<IActionResult> DeleteUser([FromQuery] int id)
        {
            var result = await _userService.DeleteUserAsync(id);
            if (!result.Success)
                return BadRequest(result.Message);

            return Ok(result);
        }

        [HttpGet("{id}/roles")]
        public async Task<ActionResult<UserRolesModel>> GetUserWithRolesByIdAsync(int id)
        {
            var result = await _userService.GetUserWithRolesByIdAsync(id);
            return Ok(result);
        }
    }
}
