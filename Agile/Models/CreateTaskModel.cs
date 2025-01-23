namespace Agile.Models
{
    public class CreateTaskModel
    {
        public string Title { get; set; }
        public string Description { get; set; }
        public int ProjectId { get; set; }
        public int StoryPoints { get; set; }
    }
}
