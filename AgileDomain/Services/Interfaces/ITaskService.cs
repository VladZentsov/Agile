using AgileDataAccess.Entities;
using AgileDomain.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AgileDomain.Services.Interfaces
{
    public interface ITaskService
    {
        Task<TaskDto> CreateTaskAsync(string title, string description, int projectId, int storyPoints);
        Task<TaskDto> UpdateTaskAsync(int taskId, string title, string description,
            TaskItemStatus status, TaskPriority taskPriority, int storyPoints);
        Task DeleteTaskAsync(int taskId);
        Task<List<TaskDto>> GetProjectTasksAsync(int projectId);
        Task<TaskDto> GetTaskDetailsAsync(int taskId);
        Task AssignTaskToUserAsync(int taskId, int userId);
        Task UnassignTaskFromUserAsync(int taskId, int userId);
        Task AssignTaskToSprintAsync(int taskId, int sprintId);
        Task<List<TaskDto>> GetAllTasks();
        Task UnAssignTaskFromSprintAsync(int taskId);
        Task<List<TaskDto>> GetUserTasks(int userId);
    }
}
