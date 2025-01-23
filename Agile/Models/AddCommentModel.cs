namespace Agile.Models
{
    public class AddCommentModel
    {
        public string Content { get; set; }
        public int EntityId { get; set; }
        public string EntityType { get; set; } // Project, Sprint, Task
        public int UserId { get; set; }
    }
}
