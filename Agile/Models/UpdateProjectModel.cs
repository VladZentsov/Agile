using AgileDataAccess.Entities;

namespace Agile.Models
{
    public class UpdateProjectModel
    {
        public int Id { get; set; }
        public required string Name { get; set; }
        public required string Description { get; set; }
        public required ProjectStatus ProjectStatus { get; set; }
    }
}
