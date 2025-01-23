using Agile.Models;
using AgileDataAccess.Entities;
using AgileDomain.Models;
using AgileDomain.Services.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace Agile.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class UsersController : ControllerBase
    {
        private readonly IUserService _userService;

        public UsersController(IUserService userService)
        {
            _userService = userService;
        }

        [HttpPost("register")]
        public async Task<IActionResult> Register([FromBody] RegisterDto registerDto)
        {
            var user = await _userService.CreateUserAsync(registerDto.FullName, registerDto.Email, registerDto.Position, registerDto.Password);
            return CreatedAtAction(nameof(GetUserById), new { id = user.Id }, user);
        }

        [HttpGet]
        public async Task<IActionResult> GetUsers()
        {
            var users = await _userService.GetAllUsersAsync();
            return Ok(users);
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetUserById(int id)
        {
            var user = await _userService.GetUserDetailsAsync(id);
            return user != null ? Ok(user) : NotFound();
        }
        [HttpGet("GetUserProjects/{id}")]
        public async Task<IActionResult> GetUserProjects(int id)
        {
            var res = await _userService.GetUserProjectsAsync(id);
            return Ok(res);
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateUser([FromBody] UpdateUserModel model)
        {
            var updatedUser = await _userService.UpdateUserAsync(model.Id, model.FullName, model.Email, model.Position);
            return Ok(updatedUser);
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteUser(int id)
        {
            await _userService.DeleteUserAsync(id);
            return NoContent();
        }

        [HttpPost]
        [Route("attachUserToProject")]
        public async Task<IActionResult> AttachUserToProject([FromBody] AttachUserToProjectModel attachUserToProjectModel)
        {
            await _userService.AttachUserToProject(attachUserToProjectModel.UserId, attachUserToProjectModel.ProjectId, attachUserToProjectModel.Role);
            return Ok();
        }
        [HttpPut]
        [Route("updateUserRole")]
        public async Task<IActionResult> UpdateUserRoleInProject([FromBody] UpdateUserRoleModel updateUserRoleModel)
        {
            await _userService.UpdateUserRoleInProjectAsync(updateUserRoleModel.UserId, updateUserRoleModel.ProjectId, updateUserRoleModel.Role);
            return Ok();
        }

        [HttpDelete]
        [Route("excludeUserFromProject/{userId}/{projectId}")]
        public async Task<IActionResult> ExcludeUserFromProject(int userId, int projectId)
        {
            await _userService.ExcludeUserFromProject(userId, projectId);
            return Ok();
        }

        [HttpPost("Login")]
        public async Task<bool> CheckPassword(LoginModel loginModel)
        {
            return await _userService.CheckPassword(loginModel.Email, loginModel.Password);
        }
    }
}
