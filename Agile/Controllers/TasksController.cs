using Agile.Models;
using AgileDataAccess.Entities;
using AgileDomain.Models;
using AgileDomain.Services.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace Agile.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class TasksController : ControllerBase
    {
        private readonly ITaskService _taskService;

        public TasksController(ITaskService taskService)
        {
            _taskService = taskService;
        }

        [HttpPost]
        public async Task<IActionResult> CreateTask([FromBody] CreateTaskModel taskDto)
        {
            var task = await _taskService.CreateTaskAsync(taskDto.Title, taskDto.Description, 
                taskDto.ProjectId, taskDto.StoryPoints);
            return CreatedAtAction(nameof(GetTaskById), new { id = task.Id }, task);
        }

        [HttpGet("project/{projectId}")]
        public async Task<IActionResult> GetTasksByProjectId(int projectId)
        {
            var tasks = await _taskService.GetProjectTasksAsync(projectId);
            return Ok(tasks);
        }

        [HttpGet("user/{userId}")]
        public async Task<IActionResult> GetUserTasks(int userId)
        {
            var tasks = await _taskService.GetUserTasks(userId);
            return Ok(tasks);
        }

        [HttpGet]
        public async Task<IActionResult> GetAll()
        {
            var task = await _taskService.GetAllTasks();
            return task != null ? Ok(task) : NotFound();
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetTaskById(int id)
        {
            var task = await _taskService.GetTaskDetailsAsync(id);
            return task != null ? Ok(task) : NotFound();
        }

        [HttpPut]
        public async Task<IActionResult> UpdateTask([FromBody] UpdateTaskModel model)
        {
            var updatedTask = await _taskService.UpdateTaskAsync(model.Id, model.Title, model.Description,
                model.Status, model.TaskPriority, model.StoryPoints);
            return Ok(updatedTask);
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteTask(int id)
        {
            await _taskService.DeleteTaskAsync(id);
            return NoContent();
        }

        [HttpPost("AssignTaskToUser")]
        public async Task<IActionResult> AssignTaskToUser(AssignTaskToUserModel assignTaskToUserModel)
        {
            await _taskService.AssignTaskToUserAsync(assignTaskToUserModel.TaskId, assignTaskToUserModel.UserId);
            return Ok();
        }

        [HttpPost("UnassignTaskFromUser")]
        public async Task<IActionResult> UnassignTaskFromUser(AssignTaskToUserModel assignTaskToUserModel)
        {
            await _taskService.UnassignTaskFromUserAsync(assignTaskToUserModel.TaskId, assignTaskToUserModel.UserId);
            return Ok();
        }

        [HttpPost("AssignTaskToSprint")]
        public async Task<IActionResult> AssignTaskToSprintAsync(AssignTaskToSprintModel assignTaskToSprintModel)
        {
            await _taskService.AssignTaskToSprintAsync(assignTaskToSprintModel.TaskId, assignTaskToSprintModel.SprintId);
            return Ok();
        }

        [HttpPost("UnAssignTaskFromSprint")]
        public async Task<IActionResult> UnAssignTaskFromSprintAsync(int taskId)
        {
            await _taskService.UnAssignTaskFromSprintAsync(taskId);
            return Ok();
        }
    }

}
