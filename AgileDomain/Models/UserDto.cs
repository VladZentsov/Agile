using AgileDataAccess.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AgileDomain.Models
{
    public class UserDto
    {
        public int Id { get; set; }
        public string FullName { get; set; }
        public string Email { get; set; } // Для управления пользователями
        public UserPosition Position { get; set; } // Для указания ранга (Junior, Middle, Senior)
        public List<ProjectRoleDto> Projects { get; set; } // Список проектов с ролями
    }
}
