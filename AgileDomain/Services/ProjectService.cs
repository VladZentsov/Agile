using AgileDataAccess.Entities;
using AgileDataAccess.UoW;
using AgileDomain.Models;
using AgileDomain.Services.Interfaces;
using System.Collections.Generic;


namespace AgileDomain.Services
{
    public class ProjectService : IProjectService
    {
        private readonly IUnitOfWork _unitOfWork;

        public ProjectService(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        public async Task<ProjectDto> CreateProjectAsync(string name, string description)
        {
            var project = new Project
            {
                Name = name,
                Description = description,
                Status = ProjectStatus.Planned,
                Created = DateTime.UtcNow,
                Updated = DateTime.UtcNow
            };

            await _unitOfWork.ProjectRepository.AddAsync(project);
            await _unitOfWork.SaveChangesAsync();

            return new ProjectDto
            {
                Id = project.ProjectID,
                Name = project.Name,
                Description = project.Description,
                Status = project.Status.ToString(),
                Created = project.Created,
                Updated = project.Updated
            };
        }

        public async Task<ProjectDto> UpdateProjectAsync(int projectId, string name, string description, ProjectStatus projectStatus)
        {
            var project = await _unitOfWork.ProjectRepository.GetByIdAsync(projectId);
            if (project == null)
                throw new KeyNotFoundException("Project not found");

            project.Name = name;
            project.Description = description;
            project.Updated = DateTime.UtcNow;
            project.Status = projectStatus;

            _unitOfWork.ProjectRepository.Update(project);
            await _unitOfWork.SaveChangesAsync();

            return new ProjectDto
            {
                Id = project.ProjectID,
                Name = project.Name,
                Description = project.Description,
                Status = project.Status.ToString(),
                Created = project.Created,
                Updated = project.Updated
            };
        }

        public async Task DeleteProjectAsync(int projectId)
        {
            var project = await _unitOfWork.ProjectRepository.GetByIdAsync(projectId);
            if (project == null)
                throw new KeyNotFoundException("Project not found");

            _unitOfWork.ProjectRepository.Remove(project);
            await _unitOfWork.SaveChangesAsync();
        }

        public async Task<List<ProjectDto>> GetAllProjectsAsync()
        {
            var projects = await _unitOfWork.ProjectRepository.GetAllAsync();
            return projects.Select(project => new ProjectDto
            {
                Id = project.ProjectID,
                Name = project.Name,
                Description = project.Description,
                Status = project.Status.ToString(),
                Created = project.Created,
                Updated = project.Updated
            }).ToList();
        }

        public async Task<ProjectDto> GetProjectDetailsAsync(int projectId)
        {
            var project = await _unitOfWork.ProjectRepository.GetByIdAsync(projectId);
            if (project == null)
                throw new KeyNotFoundException("Project not found");

            return new ProjectDto
            {
                Id = project.ProjectID,
                Name = project.Name,
                Description = project.Description,
                Status = project.Status.ToString(),
                Created = project.Created,
                Updated = project.Updated
            };
        }

        public async Task<List<ProjectUsersDto>> GetProjectUsersAsync(int projectId)
        {
            var projectTeams = await _unitOfWork.ProjectTeamRepository.FindAsync(pt => pt.ProjectID == projectId);

            List < ProjectUsersDto > res = new List < ProjectUsersDto >();

            foreach (var pt in projectTeams)
            {
                string fullName = (await _unitOfWork.UserRepository.GetByIdAsync(pt.UserID)).FullName;
                res.Add(new ProjectUsersDto()
                {
                    Id = pt.UserID,
                    FullName = fullName,
                    Role = pt.Role.ToString()
                });
            }

            return res;
        }

        public async Task UpdateProjectStatusAsync(int projectId, ProjectStatus status)
        {
            var project = await _unitOfWork.ProjectRepository.GetByIdAsync(projectId);
            if (project == null)
                throw new KeyNotFoundException("Project not found");

            project.Status = status;
            project.Updated = DateTime.UtcNow;

            _unitOfWork.ProjectRepository.Update(project);
            await _unitOfWork.SaveChangesAsync();
        }

    }
}
