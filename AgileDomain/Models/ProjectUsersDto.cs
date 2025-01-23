using AgileDataAccess.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AgileDomain.Models
{
    public class ProjectUsersDto
    {
        public int Id { get; set; }
        public string FullName { get; set; }
        public string Email { get; set; } // Для управления пользователями
        public UserPosition Position { get; set; } // Для указания ранга (Junior, Middle, Senior)
        public string Role { get; set; } // Для указания роли в проекте (Scrum Master, Manager, Developer)
    }
}
