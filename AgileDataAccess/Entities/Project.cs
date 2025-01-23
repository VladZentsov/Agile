namespace AgileDataAccess.Entities
{
    public interface IEntity
    {
        DateTime Updated { get; }
    }
    public class Project: IEntity
    {
        public int ProjectID { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public ProjectStatus Status { get; set; }
        public DateTime Created { get; set; }
        public DateTime Updated { get; set; }

        public ICollection<ProjectTeam> ProjectTeams { get; set; }
        public ICollection<TaskItem> Tasks { get; set; }
        public ICollection<Sprint> Sprints { get; set; }
    }

    public class TaskItem: IEntity
    {
        public int TaskID { get; set; }
        public int ProjectID { get; set; }
        public string Title { get; set; }
        public string Description { get; set; }
        public TaskItemStatus Status { get; set; }
        public TaskPriority Priority { get; set; }
        public int StoryPoints { get; set; }
        public int? SprintID { get; set; }
        public DateTime Updated { get; set; }

        public Project Project { get; set; }
        public Sprint Sprint { get; set; }
        public ICollection<TaskTeam> TaskTeams { get; set; }
    }

    public class Sprint: IEntity
    {
        public int SprintID { get; set; }
        public int ProjectID { get; set; }
        public string Title { get; set; }
        public string Description { get; set; }
        public DateTime StartDate { get; set; }
        public DateTime EndDate { get; set; }
        public DateTime Updated { get; set; }

        public Project Project { get; set; }
        public ICollection<TaskItem> Tasks { get; set; }
    }

    public class User
    {
        public int UserID { get; set; }
        public string FullName { get; set; }
        public string Email { get; set; }
        public string Password { get; set; }
        public UserPosition Position { get; set; }

        public ICollection<ProjectTeam> ProjectTeams { get; set; }
        public ICollection<TaskTeam> TaskTeams { get; set; }
        public ICollection<Comment> Comments { get; set; }
    }

    public class ProjectTeam
    {
        public int ProjectTeamID { get; set; }
        public int ProjectID { get; set; }
        public int UserID { get; set; }
        public ProjectRole Role { get; set; }
        public DateTime Updated { get; set; }

        public Project Project { get; set; }
        public User User { get; set; }
    }

    public class TaskTeam
    {
        public int TaskTeamID { get; set; }
        public int TaskID { get; set; }
        public int UserID { get; set; }
        public DateTime Updated { get; set; }

        public TaskItem Task { get; set; }
        public User User { get; set; }
    }

    public class Comment
    {
        public int CommentID { get; set; }
        public EntityType EntityType { get; set; }
        public int EntityID { get; set; }
        public int UserID { get; set; }
        public string Content { get; set; }
        public DateTime Created { get; set; }
        public DateTime Updated { get; set; }

        public User User { get; set; }
    }

    // Enums
    public enum ProjectStatus
    {
        Planned,
        InProgress,
        Completed
    }

    public enum TaskItemStatus
    {
        OnHold,
        Current,
        Done,
        Testing
    }

    public enum TaskPriority
    {
        Low,
        Medium,
        High,
        VeryHigh
    }

    public enum UserPosition
    {
        Junior,
        Middle,
        Senior
    }

    public enum ProjectRole
    {
        ScrumMaster,
        Manager,
        Developer
    }

    public enum EntityType
    {
        Task,
        Sprint,
        Project
    }
}
