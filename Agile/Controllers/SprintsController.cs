using Agile.Models;
using AgileDomain.Models;
using AgileDomain.Services.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace Agile.Controllers
{

    [ApiController]
    [Route("api/[controller]")]
    public class SprintsController : ControllerBase
    {
        private readonly ISprintService _sprintService;

        public SprintsController(ISprintService sprintService)
        {
            _sprintService = sprintService;
        }

        [HttpPost]
        public async Task<IActionResult> CreateSprint([FromBody] CreateSprintModel sprintDto)
        {
            var sprint = await _sprintService.CreateSprintAsync(sprintDto.Title, sprintDto.Description, sprintDto.StartDate, sprintDto.EndDate, sprintDto.ProjectId);
            return CreatedAtAction(nameof(GetSprintById), new { id = sprint.Id }, sprint);
        }

        [HttpGet("project/{projectId}")]
        public async Task<IActionResult> GetSprintsByProjectId(int projectId)
        {
            var sprints = await _sprintService.GetProjectSprintsAsync(projectId);
            return Ok(sprints);
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetSprintById(int id)
        {
            var sprint = await _sprintService.GetSprintByIdAsync(id);
            return sprint != null ? Ok(sprint) : NotFound();
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateSprint([FromBody] UpdateSprintModel sprintDto)
        {
            var updatedSprint = await _sprintService.UpdateSprintAsync(sprintDto.Id, sprintDto.Title, sprintDto.Description, sprintDto.StartDate, sprintDto.EndDate);
            return Ok(updatedSprint);
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteSprint(int id)
        {
            await _sprintService.DeleteSprintAsync(id);
            return NoContent();
        }
    }
}
