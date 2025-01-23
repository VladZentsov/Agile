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
    public class SprintService : ISprintService
    {
        private readonly IUnitOfWork _unitOfWork;

        public SprintService(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        public async Task<SprintDto> CreateSprintAsync(string title, string description, DateTime startDate, DateTime endDate, int projectId)
        {
            var sprint = new Sprint
            {
                Title = title,
                Description = description,
                StartDate = startDate,
                EndDate = endDate,
                ProjectID = projectId
            };

            await _unitOfWork.SprintRepository.AddAsync(sprint);
            await _unitOfWork.SaveChangesAsync();

            return new SprintDto
            {
                Id = sprint.SprintID,
                Title = sprint.Title,
                Description = sprint.Description,
                StartDate = sprint.StartDate,
                EndDate = sprint.EndDate,
                ProjectId = sprint.ProjectID,
                ProjectName = sprint.Project?.Name
            };
        }

        public async Task<SprintDto> UpdateSprintAsync(int sprintId, string title, string description, DateTime startDate, DateTime endDate)
        {
            var sprint = await _unitOfWork.SprintRepository.GetByIdAsync(sprintId);
            if (sprint == null)
                throw new KeyNotFoundException("Sprint not found");

            sprint.Title = title;
            sprint.Description = description;
            sprint.StartDate = startDate;
            sprint.EndDate = endDate;

            _unitOfWork.SprintRepository.Update(sprint);
            await _unitOfWork.SaveChangesAsync();

            return new SprintDto
            {
                Id = sprint.SprintID,
                Title = sprint.Title,
                Description = sprint.Description,
                StartDate = sprint.StartDate,
                EndDate = sprint.EndDate,
                ProjectId = sprint.ProjectID,
                ProjectName = sprint.Project?.Name
            };
        }

        public async Task DeleteSprintAsync(int sprintId)
        {
            var sprint = await _unitOfWork.SprintRepository.GetByIdAsync(sprintId);
            if (sprint == null)
                throw new KeyNotFoundException("Sprint not found");

            _unitOfWork.SprintRepository.Remove(sprint);
            await _unitOfWork.SaveChangesAsync();
        }

        public async Task<List<SprintDto>> GetProjectSprintsAsync(int projectId)
        {
            var sprints = await _unitOfWork.SprintRepository.FindAsync(s => s.ProjectID == projectId);

            return sprints.Select(sprint => new SprintDto
            {
                Id = sprint.SprintID,
                Title = sprint.Title,
                Description = sprint.Description,
                StartDate = sprint.StartDate,
                EndDate = sprint.EndDate,
                ProjectId = sprint.ProjectID,
                ProjectName = sprint.Project?.Name
            }).ToList();
        }

        public async Task<List<SprintDto>> GetSprintByIdAsync(int id)
        {
            var sprints = await _unitOfWork.SprintRepository.FindAsync(s => s.SprintID == id);

            return sprints.Select(sprint => new SprintDto
            {
                Id = sprint.SprintID,
                Title = sprint.Title,
                Description = sprint.Description,
                StartDate = sprint.StartDate,
                EndDate = sprint.EndDate,
                ProjectId = sprint.ProjectID,
                ProjectName = sprint.Project?.Name
            }).ToList();
        }

        public async Task<List<TaskDto>> GetSprintTasksAsync(int sprintId)
        {
            var tasks = await _unitOfWork.TaskRepository.FindAsync(t => t.SprintID == sprintId);

            return tasks.Select(task => new TaskDto
            {
                Id = task.TaskID,
                Title = task.Title,
                Description = task.Description,
                Status = task.Status.ToString(),
                Priority = task.Priority.ToString(),
                //Created = task.Created,
                Updated = task.Updated
            }).ToList();
        }
    }
}
