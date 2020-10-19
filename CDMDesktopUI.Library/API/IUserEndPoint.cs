using CDMDesktopUI.Library.Models;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace CDMDesktopUI.Library.API
{
    public interface IUserEndPoint
    {
        Task AddUserToRole(string userId, string roleId);
        Task<Dictionary<string, string>> GetRoles();
        Task<List<AppUserModel>> GetUsers();
        Task RemoveUserFromRole(string userId, string roleId);
    }
}