using AgileDataAccess.Entities;
using AgileDataAccess.UoW;
using AgileDomain.Models;
using AgileDomain.Services.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AgileDomain.Services
{
    public class UserService : IUserService
    {
        private readonly IUnitOfWork _unitOfWork;

        public UserService(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        public async Task<bool> CheckPassword(string email, string password)
        {
            var user = await _unitOfWork.UserRepository.FindAsync(u=>u.Email == email && u.Password == password);
            if (user == null || user.Count() == 0)
            {
                return false;
            }
            return true;
        }

        public async Task<UserDto> CreateUserAsync(string fullName, string email, UserPosition position, string password)
        {
            var user = new User
            {
                FullName = fullName,
                Email = email,
                Position = position,
                Password = password
            };

            await _unitOfWork.UserRepository.AddAsync(user);
            await _unitOfWork.SaveChangesAsync();

            return new UserDto
            {
                Id = user.UserID,
                FullName = user.FullName,
                Email = user.Email,
                Position = user.Position
            };
        }

        public async Task<UserDto> UpdateUserAsync(int userId, string fullName, string email, string position)
        {
            var user = await _unitOfWork.UserRepository.GetByIdAsync(userId);
            if (user == null)
                throw new KeyNotFoundException("User not found");

            user.FullName = fullName;
            user.Email = email;
            user.Position = Enum.Parse<UserPosition>(position, true);

            _unitOfWork.UserRepository.Update(user);
            await _unitOfWork.SaveChangesAsync();

            return new UserDto
            {
                Id = user.UserID,
                FullName = user.FullName,
                Email = user.Email,
                Position = user.Position
            };
        }

        public async Task DeleteUserAsync(int userId)
        {
            var user = await _unitOfWork.UserRepository.GetByIdAsync(userId);
            if (user == null)
                throw new KeyNotFoundException("User not found");

            _unitOfWork.UserRepository.Remove(user);
            await _unitOfWork.SaveChangesAsync();
        }

        public async Task<List<UserDto>> GetAllUsersAsync()
        {
            var users = await _unitOfWork.UserRepository.GetAllAsync();
            return users.Select(user => new UserDto
            {
                Id = user.UserID,
                FullName = user.FullName,
                Email = user.Email,
                Position = user.Position
            }).ToList();
        }

        public async Task<UserDto> GetUserDetailsAsync(int userId)
        {
            var user = await _unitOfWork.UserRepository.GetByIdAsync(userId);
            if (user == null)
                throw new KeyNotFoundException("User not found");

            return new UserDto
            {
                Id = user.UserID,
                FullName = user.FullName,
                Email = user.Email,
                Position = user.Position
            };
        }

        public async Task<List<ProjectRoleDto>> GetUserProjectsAsync(int userId)
        {
            var projectTeams = await _unitOfWork.ProjectTeamRepository.FindAsync(pt => pt.UserID == userId);

            return projectTeams.Select(pt => new ProjectRoleDto
            {
                ProjectId = pt.ProjectID,
                ProjectName = pt.Project.Name,
                Role = pt.Role.ToString()
            }).ToList();
        }

        public async Task UpdateUserRoleInProjectAsync(int userId, int projectId, ProjectRole role)
        {
            var projectTeam = (await _unitOfWork.ProjectTeamRepository.FindAsync(pt => pt.UserID == userId && pt.ProjectID == projectId)).FirstOrDefault();
            if (projectTeam == null)
                throw new KeyNotFoundException("User is not assigned to the project");

            projectTeam.Role = role;

            _unitOfWork.ProjectTeamRepository.Update(projectTeam);
            await _unitOfWork.SaveChangesAsync();
        }

        public async Task AttachUserToProject(int userId, int projectId, ProjectRole role)
        {
            ProjectTeam projectTeam = new ProjectTeam()
            {
                ProjectID = projectId,
                UserID = userId,
                Role = role
            };

            await _unitOfWork.ProjectTeamRepository.AddAsync(projectTeam);
            await _unitOfWork.SaveChangesAsync();
        }

        public async Task ExcludeUserFromProject(int userId, int projectId)
        {
            var projectTeam = (await _unitOfWork.ProjectTeamRepository.GetAllAsync())
                .FirstOrDefault(u => u.ProjectID == projectId && u.UserID == userId);

            _unitOfWork.ProjectTeamRepository.Remove(projectTeam);
            await _unitOfWork.SaveChangesAsync();
        }
    }
}
