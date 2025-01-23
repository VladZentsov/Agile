using AgileDataAccess.Entities;

namespace Agile.Models
{
    public class AttachUserToProjectModel
    {
        public int UserId { get; set; }
        public int ProjectId { get; set; }
        public ProjectRole Role { get; set; }
    }
}
