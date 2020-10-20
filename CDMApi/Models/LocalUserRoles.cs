using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CDMApi.Models
{
    public class LocalUserRoles
    {
        // ur.UserId, ur.RoleId, r.Name 
        public string UserId { get; set; }
        public string RoleId { get; set; }
        public string Name { get; set; }
    }
}
