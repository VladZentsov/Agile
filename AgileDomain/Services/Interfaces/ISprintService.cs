using AgileDomain.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AgileDomain.Services.Interfaces
{
    public interface ISprintService
    {
        Task<SprintDto> CreateSprintAsync(string title, string description, DateTime startDate, DateTime endDate, int projectId);
        Task<SprintDto> UpdateSprintAsync(int sprintId, string title, string description, DateTime startDate, DateTime endDate);
        Task DeleteSprintAsync(int sprintId);
        Task<List<SprintDto>> GetProjectSprintsAsync(int projectId);
        Task<List<TaskDto>> GetSprintTasksAsync(int sprintId);
        Task<List<SprintDto>> GetSprintByIdAsync(int id);
    }
}
