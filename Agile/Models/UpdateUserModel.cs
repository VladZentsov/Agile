namespace Agile.Models
{
    public class UpdateUserModel
    {
        public int Id { get; set; } 
        public string FullName { get; set; }
        public string Email { get; set; }
        public string Position { get; set; } // Junior, Middle, Senior
    }
}
