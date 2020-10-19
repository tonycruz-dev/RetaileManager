using CDMLibrary.DataAccess;
using CruzDataManager.Models;
using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.EntityFramework;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;

namespace CruzDataManager.Controllers
{
   // [Authorize]
    [RoutePrefix("api/User")]
    public class UserController : ApiController
    {
        [Route("UserById")]
        [HttpGet]
        public UserModel UserById()
        {
            string id = RequestContext.Principal.Identity.GetUserId();
            UserData data = new UserData();
           return  data.GetUserById(id).First();
        }
        [Authorize(Roles = "Admin")] 
        [Route("GetAllUsers")]
        public List<AppUserModel> GetAllUsers()
        {
            using (var context = new ApplicationDbContext())
            {
                var userStore = new UserStore<ApplicationUser>(context);
                var userManager = new UserManager<ApplicationUser>(userStore);
                var roles = context.Roles.ToList();

               
                var users = userManager.Users.Select(u => new AppUserModel()
                {
                    Email = u.Email,
                    UserName = u.UserName,
                    Id = u.Id,
                    Roles = u.Roles.ToList(),
                   // UserRols = u.Roles.Select(r => new UserRoleDto() {RoleId = r.RoleId, RoleName = roles.FirstOrDefault(rf => rf.Id == r.RoleId).Name}).ToList()

                }).ToList() ;

                foreach (var user in users)
                {
                    foreach (var role in user.Roles)
                    {
                        user.UserRols.Add(
                            new UserRoleDto
                            {
                                RoleId = role.RoleId,
                                RoleName = roles.FirstOrDefault(r => r.Id == role.RoleId).Name
                            });
                        if (string.IsNullOrWhiteSpace(user.CurrentRoles))
                        {
                            user.CurrentRoles = roles.FirstOrDefault(r => r.Id == role.RoleId).Name;
                        }
                        else
                        {
                            user.CurrentRoles = user.CurrentRoles + ", " + roles.FirstOrDefault(r => r.Id == role.RoleId).Name;
                        }
                        
                    }
                }
                // var roles = context.Roles.ToList();
                return users;
            }
        }
        [Authorize(Roles = "Admin")]
        [Route("GetAllRoles")]
        public List<UserRoleDto> GetAllRoles()
        {
            using (var context = new ApplicationDbContext())
            {
                // UserRols = u.Roles.Select(r => new UserRoleDto() {RoleId = r.RoleId, RoleName = roles.FirstOrDefault(rf => rf.Id == r.RoleId).Name}).ToList()

                var roles = context.Roles.Select(r => new UserRoleDto() { RoleId = r.Id, RoleName = r.Name }).ToList();
                return roles;
            }
        }
        [Route("GetRoles")]
        public Dictionary<string,string> GetRoles()
        {
            using (var context = new ApplicationDbContext())
            {
                // UserRols = u.Roles.Select(r => new UserRoleDto() {RoleId = r.RoleId, RoleName = roles.FirstOrDefault(rf => rf.Id == r.RoleId).Name}).ToList()

                var roles = context.Roles.ToDictionary(x => x.Id, x => x.Name); // .Select(r => new UserRoleDto() { RoleId = r.Id, RoleName = r.Name }).ToList();
                return roles;
            }
        }
        [Authorize(Roles = "Admin")]
        [HttpPost]
        [Route("AddUserToRole")]
        public void AddRole(UserRoleDataDto userRole)
        {
            using (var context = new ApplicationDbContext())
            {
                var userStore = new UserStore<ApplicationUser>(context);
                var userManager = new UserManager<ApplicationUser>(userStore);

                userManager.AddToRole(userRole.UserId, userRole.Role);

             
            }
        }
        [Authorize(Roles = "Admin")]
        [HttpPost]
        [Route("RemoveRole")]
        public void RemoveRole(UserRoleDataDto userRole)
        {
            using (var context = new ApplicationDbContext())
            {
                var userStore = new UserStore<ApplicationUser>(context);
                var userManager = new UserManager<ApplicationUser>(userStore);

                userManager.RemoveFromRole(userRole.UserId, userRole.Role);

            }
        }
    }
}
