using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CDMDesktopUI.Library.Models
{
    public class AppUserModel
    {
        public string Id { get; set; }
        public string Email { get; set; }
        public string UserName { get; set; }
        public string CurrentRoles { get; set; }
        public List<UserRoleDto> UserRols { get; set; } 
    }
    public class UserRoleDto
    {
        public string RoleId { get; set; }
        public string RoleName { get; set; }
    }
}
