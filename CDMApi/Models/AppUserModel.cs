using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CDMApi.Models
{
    public class AppUserModel
    {
        public string Id { get; set; }
        public string Email { get; set; }
        public string UserName { get; set; }
        public string CurrentRoles { get; set; }
        public List<UserRoleDto> UserRols { get; set; } = new List<UserRoleDto>();
        //public List<IdentityUserRole> Roles { get; set; }
    }
    public class UserRoleDto
    {
        public string RoleId { get; set; }
        public string RoleName { get; set; }
    }
}
