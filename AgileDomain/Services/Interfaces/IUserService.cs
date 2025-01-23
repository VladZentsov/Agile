using AgileDataAccess.Entities;
using AgileDomain.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AgileDomain.Services.Interfaces
{
    public interface IUserService
    {
        Task<UserDto> CreateUserAsync(string fullName, string email, UserPosition position, string password);
        Task<UserDto> UpdateUserAsync(int userId, string fullName, string email, string position);
        Task DeleteUserAsync(int userId);
        Task<List<UserDto>> GetAllUsersAsync();
        Task<UserDto> GetUserDetailsAsync(int userId);
        Task<List<ProjectRoleDto>> GetUserProjectsAsync(int userId);
        Task UpdateUserRoleInProjectAsync(int userId, int projectId, ProjectRole role);
        Task AttachUserToProject(int userId, int projectId, ProjectRole role);
        Task ExcludeUserFromProject(int userId, int projectId);
        Task<bool> CheckPassword(string email, string password);
    }
}
