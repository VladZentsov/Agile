using AgileDataAccess.Entities;
using AgileDataAccess.UoW;
using AgileDomain.Models;
using AgileDomain.Services.Interfaces;

namespace AgileDomain.Services
{
    public class TaskService : ITaskService
    {
        private readonly IUnitOfWork _unitOfWork;

        public TaskService(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        public async Task<List<TaskDto>> GetAllTasks()
        {
            var res = new List<TaskDto>();
            var tasks = await _unitOfWork.TaskRepository.GetAllAsync();
            foreach (var task in tasks)
            {
                res.Add(new TaskDto
                {
                    Id = task.TaskID,
                    Title = task.Title,
                    Description = task.Description,
                    Status = task.Status.ToString(),
                    Priority = task.Priority.ToString(),
                    //Created = task.Created,
                    Updated = task.Updated,
                    SprintId = task.SprintID,
                    StoryPoints = task.StoryPoints,
                    AssignedUsers = await GetTaskUsers(task.TaskID)
                });
            }

            return res;
        }
        public async Task<TaskDto> CreateTaskAsync(string title, string description, int projectId, int storyPoints)
        {
            var task = new TaskItem
            {
                Title = title,
                Description = description,
                Status = TaskItemStatus.OnHold,
                Priority = TaskPriority.Low,
                ProjectID = projectId,
                //Created = DateTime.UtcNow,
                Updated = DateTime.UtcNow,
                StoryPoints = storyPoints,
                
            };

            await _unitOfWork.TaskRepository.AddAsync(task);
            await _unitOfWork.SaveChangesAsync();

            return new TaskDto
            {
                Id = task.TaskID,
                Title = task.Title,
                Description = task.Description,
                Status = task.Status.ToString(),
                Priority = task.Priority.ToString(),
                //Created = task.Created,
                Updated = task.Updated,
                AssignedUsers = await GetTaskUsers(task.TaskID)
            };
        }

        public async Task<TaskDto> UpdateTaskAsync(int taskId, string title, string description, 
            TaskItemStatus status, TaskPriority taskPriority, int storyPoints)
        {
            var task = await _unitOfWork.TaskRepository.GetByIdAsync(taskId);
            if (task == null)
                throw new KeyNotFoundException("Task not found");

            task.Title = title;
            task.Description = description;
            task.Updated = DateTime.UtcNow;
            task.Status = status;
            task.Priority = taskPriority;
            task.StoryPoints = storyPoints;

            _unitOfWork.TaskRepository.Update(task);
            await _unitOfWork.SaveChangesAsync();

            return new TaskDto
            {
                Id = task.TaskID,
                Title = task.Title,
                Description = task.Description,
                Status = task.Status.ToString(),
                Priority = task.Priority.ToString(),
                //Created = task.Created,
                Updated = task.Updated,
                StoryPoints = storyPoints,
                AssignedUsers = await GetTaskUsers(task.TaskID)
            };
        }

        public async Task DeleteTaskAsync(int taskId)
        {
            var task = await _unitOfWork.TaskRepository.GetByIdAsync(taskId);
            if (task == null)
                throw new KeyNotFoundException("Task not found");

            _unitOfWork.TaskRepository.Remove(task);
            await _unitOfWork.SaveChangesAsync();
        }

        public async Task<List<TaskDto>> GetProjectTasksAsync(int projectId)
        {
            var tasks = await _unitOfWork.TaskRepository.FindAsync(t => t.ProjectID == projectId);
            var res = new List<TaskDto>();
            foreach (var task in tasks)
            {
                res.Add(new TaskDto
                {
                    Id = task.TaskID,
                    Title = task.Title,
                    Description = task.Description,
                    Status = task.Status.ToString(),
                    Priority = task.Priority.ToString(),
                    //Created = task.Created,
                    Updated = task.Updated,
                    SprintId = task.SprintID,
                    StoryPoints = task.StoryPoints,
                    AssignedUsers = await GetTaskUsers(task.TaskID)
                });
            }
            return res;
        }

        public async Task<List<TaskDto>> GetUserTasks(int userId)
        {
            var res = new List<TaskDto>();
            var taskIds = (await _unitOfWork.TaskTeamRepository.FindAsync(ut=>ut.UserID == userId))?
                                    .Select(ut=>ut.TaskID);
            
            if(taskIds == null)
                return res;

            var tasks = await _unitOfWork.TaskRepository.FindAsync(t => taskIds.Contains(t.TaskID));
            foreach (var task in tasks)
            {
                res.Add(new TaskDto
                {
                    Id = task.TaskID,
                    Title = task.Title,
                    Description = task.Description,
                    Status = task.Status.ToString(),
                    Priority = task.Priority.ToString(),
                    //Created = task.Created,
                    Updated = task.Updated,
                    SprintId = task.SprintID,
                    StoryPoints = task.StoryPoints,
                    AssignedUsers = await GetTaskUsers(task.TaskID)
                });
            }

            return res;
        }

        public async Task<TaskDto> GetTaskDetailsAsync(int taskId)
        {
            var task = await _unitOfWork.TaskRepository.GetByIdAsync(taskId);
            if (task == null)
                throw new KeyNotFoundException("Task not found");

            return new TaskDto
            {
                Id = task.TaskID,
                Title = task.Title,
                Description = task.Description,
                Status = task.Status.ToString(),
                Priority = task.Priority.ToString(),
                SprintId = task.SprintID,
                SprintTitle = task.Sprint?.Title,
                AssignedUsers = await GetTaskUsers(task.TaskID),
                //Created = task.Created,
                Updated = task.Updated,
                StoryPoints = task.StoryPoints
            };
        }
        public async Task AssignTaskToUserAsync(int taskId, int userId)
        {
            var task = await _unitOfWork.TaskRepository.GetByIdAsync(taskId);
            if (task == null)
                throw new KeyNotFoundException("Task not found");

            var user = await _unitOfWork.UserRepository.GetByIdAsync(userId);
            if (user == null)
                throw new KeyNotFoundException("User not found");

            var taskTeam = new TaskTeam
            {
                TaskID = taskId,
                UserID = userId,
                Updated = DateTime.UtcNow
            };

            await _unitOfWork.TaskTeamRepository.AddAsync(taskTeam);
            await _unitOfWork.SaveChangesAsync();
        }

        public async Task UnassignTaskFromUserAsync(int taskId, int userId)
        {
            var taskTeam = (await _unitOfWork.TaskTeamRepository.FindAsync(tt => tt.TaskID == taskId && tt.UserID == userId)).FirstOrDefault();
            if (taskTeam == null)
                throw new KeyNotFoundException("Assignment not found");

            _unitOfWork.TaskTeamRepository.Remove(taskTeam);
            await _unitOfWork.SaveChangesAsync();
        }

        public async Task AssignTaskToSprintAsync(int taskId, int sprintId)
        {
            var task = await _unitOfWork.TaskRepository.GetByIdAsync(taskId);
            if (task == null)
                throw new KeyNotFoundException("Task not found");

            var sprint = await _unitOfWork.SprintRepository.GetByIdAsync(sprintId);
            if (sprint == null)
                throw new KeyNotFoundException("Sprint not found");

            task.SprintID = sprintId;
            task.Updated = DateTime.UtcNow;

            _unitOfWork.TaskRepository.Update(task);
            await _unitOfWork.SaveChangesAsync();
        }

        public async Task UnAssignTaskFromSprintAsync(int taskId)
        {
            var task = await _unitOfWork.TaskRepository.GetByIdAsync(taskId);
            if (task == null)
                throw new KeyNotFoundException("Task not found");

            task.SprintID = null;
            task.Updated = DateTime.UtcNow;

            _unitOfWork.TaskRepository.Update(task);
            await _unitOfWork.SaveChangesAsync();
        }

        public async Task<List<UserDto>> GetTaskUsers(int taskId)
        {
            var res = new List<UserDto>();
            var userIds = (await _unitOfWork.TaskTeamRepository.FindAsync(tt => tt.TaskID == taskId)).Select(tt=>tt.UserID);
            if (userIds == null)
                return res;

            var users = await _unitOfWork.UserRepository.FindAsync(u=>userIds.Contains(u.UserID));
            res = users.Select(user => new UserDto
            {
                Id = user.UserID,
                FullName = user.FullName,
                Email = user.Email,
                Position = user.Position
            }).ToList();

            return res;
        }
    }
}
