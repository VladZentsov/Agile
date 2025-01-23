using AgileDataAccess.Entities;

namespace Agile.Models
{
    public class UpdateUserRoleModel
    {
        /// <summary>
        /// ID of the user whose role needs to be updated.
        /// </summary>
        public int UserId { get; set; }

        /// <summary>
        /// ID of the project.
        /// </summary>
        public int ProjectId { get; set; }

        /// <summary>
        /// New role to be assigned to the user.
        /// </summary>
        public ProjectRole Role { get; set; }
    }
}
