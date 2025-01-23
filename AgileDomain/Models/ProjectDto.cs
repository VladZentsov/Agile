using AgileDataAccess.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AgileDomain.Models
{
    public class ProjectDto
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public string Status { get; set; }
        public DateTime Created { get; set; }
        public DateTime Updated { get; set; }
        public List<UserDto> Users { get; set; }
    }


    public class ProjectRoleDto
    {
        public int ProjectId { get; set; }
        public string ProjectName { get; set; }
        public string Role { get; set; }
    }
    public class TaskDto
    {
        public int Id { get; set; }
        public string Title { get; set; }
        public string Description { get; set; }
        public string Status { get; set; }
        public string Priority { get; set; }
        public int? SprintId { get; set; }
        public string SprintTitle { get; set; }
        public List<UserDto> AssignedUsers { get; set; }
        public DateTime Created { get; set; }
        public DateTime Updated { get; set; }
        public int StoryPoints { get; set; }
    }
    public class SprintDto
    {
        public int Id { get; set; }
        public string Title { get; set; }
        public string Description { get; set; }
        public DateTime StartDate { get; set; }
        public DateTime EndDate { get; set; }
        public int ProjectId { get; set; }
        public string ProjectName { get; set; }
        public List<TaskDto> Tasks { get; set; }
    }
    public class CommentDto
    {
        public int Id { get; set; }
        public string Content { get; set; }
        public int EntityId { get; set; }
        public string EntityType { get; set; } // Project, Sprint, Task
        public int UserId { get; set; }
        public string UserFullName { get; set; }
        public DateTime Created { get; set; }
        public DateTime Updated { get; set; }
    }

    public class RegisterDto
    {
        public string FullName { get; set; }
        public string Email { get; set; }
        public string Password { get; set; }
        public UserPosition Position { get; set; } // Junior, Middle, Senior
    }

    public class LoginDto
    {
        public string Email { get; set; }
        public string Password { get; set; }
    }

    public class AuthResponseDto
    {
        public string Token { get; set; }
        public string FullName { get; set; }
        public string Position { get; set; }
    }

    public class RoleCheckDto
    {
        public int ProjectId { get; set; }
        public string Role { get; set; } // ScrumMaster, Manager, Developer
    }

    public class SearchFilterDto
    {
        public string Keyword { get; set; }
        public string Status { get; set; }
        public string Priority { get; set; }
        public DateTime? StartDate { get; set; }
        public DateTime? EndDate { get; set; }
    }

    public class ExportDataDto
    {
        public string FileType { get; set; } // CSV or Excel
        public string EntityType { get; set; } // Project, Task, Sprint, Comment
        public int EntityId { get; set; }
    }

    public class ChangeLogDto
    {
        public int Id { get; set; }
        public string EntityType { get; set; }
        public int EntityId { get; set; }
        public string PropertyChanged { get; set; }
        public string OldValue { get; set; }
        public string NewValue { get; set; }
        public DateTime Timestamp { get; set; }
    }
}
