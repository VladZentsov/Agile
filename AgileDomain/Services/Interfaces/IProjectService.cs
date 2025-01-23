using AgileDataAccess.Entities;
using AgileDomain.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AgileDomain.Services.Interfaces
{
    public interface IProjectService
    {
        Task<ProjectDto> CreateProjectAsync(string name, string description);
        Task DeleteProjectAsync(int projectId);
        Task<List<ProjectDto>> GetAllProjectsAsync();
        Task<ProjectDto> GetProjectDetailsAsync(int projectId);
        Task<List<ProjectUsersDto>> GetProjectUsersAsync(int projectId);
        Task<ProjectDto> UpdateProjectAsync(int projectId, string name, string description, ProjectStatus projectStatus);
    }
}
