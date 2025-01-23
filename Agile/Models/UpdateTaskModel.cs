using AgileDataAccess.Entities;

namespace Agile.Models
{
    public class UpdateTaskModel
    {
        public int Id { get; set; }
        public string Title { get; set; }
        public string Description { get; set; }
        public TaskItemStatus Status { get; set; }
        public TaskPriority TaskPriority { get; set; }
        public int StoryPoints { get; set; }
    }
}
