using Agile.Models;
using AgileDomain.Models;
using AgileDomain.Services.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace Agile.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class ProjectsController : ControllerBase
    {
        private readonly IProjectService _projectService;

        public ProjectsController(IProjectService projectService)
        {
            _projectService = projectService;
        }

        [HttpPost]
        public async Task<IActionResult> CreateProject([FromBody] CreateProjectModel model)
        {
            var project = await _projectService.CreateProjectAsync(model.Name, model.Description);
            return CreatedAtAction(nameof(GetProjectById), new { id = project.Id }, project);
        }

        [HttpGet]
        public async Task<IActionResult> GetProjects()
        {
            var projects = await _projectService.GetAllProjectsAsync();
            return Ok(projects);
        }
        [HttpGet("GetProjectUsers/{projectId}")]
        public async Task<IActionResult> GetProjectUsers(int projectId)
        {
            var projects = await _projectService.GetProjectUsersAsync(projectId);
            return Ok(projects);
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetProjectById(int id)
        {
            var project = await _projectService.GetProjectDetailsAsync(id);
            return project != null ? Ok(project) : NotFound();
        }

        [HttpPut]
        public async Task<IActionResult> UpdateProject([FromBody] UpdateProjectModel model)
        {
            var updatedProject = await _projectService.UpdateProjectAsync(model.Id, model.Name, model.Description, model.ProjectStatus);
            return Ok(updatedProject);
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteProject(int id)
        {
            await _projectService.DeleteProjectAsync(id);
            return NoContent();
        }
    }
}
