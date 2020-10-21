using CDMApi.Data;
using CDMApi.Models;
using CDMLibrary.DataAccess;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;

namespace CDMApi.Controllers
{
    [Authorize]
    [Route("api/[controller]")]
    [ApiController]
    public class UsersController : ControllerBase
    {
        private readonly ApplicationDbContext _context;
        private readonly UserManager<IdentityUser> _userManager;
        private readonly IConfiguration _config;
        private readonly IUserData _userData;

        public UsersController(ApplicationDbContext context,
                               UserManager<IdentityUser> userManager,
                               IUserData userData)
        {
            _context = context;
            _userManager = userManager;
            _userData = userData;
        }
       
        [HttpGet("UserById")]
        public UserModel UserById()
        {
            string id = User.FindFirstValue(ClaimTypes.NameIdentifier);
            return _userData.GetUserById(id).First();
        }
        
        [Authorize(Roles = "Admin")]
        [HttpGet("GetAllUsers")]
        public List<AppUserModel> GetAllUsers()
        {
            var users = _context.Users.ToList();
            var roles = _context.Roles.ToList();
            var userRole = (from ur in _context.UserRoles
                           join r in _context.Roles on ur.RoleId equals r.Id
                            select new LocalUserRoles {UserId = ur.UserId, RoleId = ur.RoleId, Name = r.Name }).ToList();
            List<AppUserModel> usersToReturn = new List<AppUserModel>();
            foreach (var user in users)
            {
                AppUserModel u = new AppUserModel
                {
                    Email = user.Email,
                    UserName = user.UserName,
                    Id = user.Id,
                    UserRols = userRole.Where(ur => ur.UserId == user.Id).Select(urs => new UserRoleDto { RoleId = urs.RoleId, RoleName = urs.Name }).ToList(),
                    CurrentRoles = GetCurrentRoles(user.Id, userRole)
                };
                usersToReturn.Add(u);
            }
            return usersToReturn;
            
        }
        private string GetCurrentRoles(string userId, List<LocalUserRoles> localUserRoles)
        {
            var uRole = localUserRoles.Where(ur => ur.UserId == userId).ToList();
            string roleToReturn = "";
            foreach (var item in uRole)
            {
                if (string.IsNullOrWhiteSpace(roleToReturn))
                {
                    roleToReturn = item.Name;
                }
            }
            return roleToReturn;
        }
        [Authorize(Roles = "Admin")]
        [HttpGet("GetAllRoles")]
        public List<UserRoleDto> GetAllRoles()
        {

             var roles = _context.Roles.Select(r => new UserRoleDto() { RoleId = r.Id, RoleName = r.Name }).ToList();
             return roles;
           
        }
        [Authorize(Roles = "Admin")]
        [HttpGet("GetRoles")]
        public Dictionary<string, string> GetRoles()
        {
            var roles = _context.Roles.ToDictionary(x => x.Id, x => x.Name); // .Select(r => new UserRoleDto() { RoleId = r.Id, RoleName = r.Name }).ToList();
             return roles;
     
        }
        [Authorize(Roles = "Admin")]
        [HttpPost("AddToRole")]
        public async Task AddToRole(UserRoleDataDto userRole)
        {
            var user = await _userManager.FindByIdAsync(userRole.UserId);
            await _userManager.AddToRoleAsync(user, userRole.Role);
         }
        [Authorize(Roles = "Admin")]
        [HttpPost("RemoveRole")]
        public async Task RemoveRole(UserRoleDataDto userRole)
        {
            var user = await _userManager.FindByIdAsync(userRole.UserId);
            await _userManager.RemoveFromRoleAsync(user, userRole.Role);

        }
       // [Authorize(Roles = "Admin")]
        [AllowAnonymous]
        [HttpPost("CreateNewRole")]
        public async Task AddNewRole(Models.UserRoleDataDto userRole)
        {
            var user = await _userManager.FindByIdAsync(userRole.UserId);
            await _userManager.AddToRoleAsync(user, userRole.Role);
        }
    }
}
