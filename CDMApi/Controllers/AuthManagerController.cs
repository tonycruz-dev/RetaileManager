using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;

namespace CDMApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AuthManagerController : ControllerBase
    {
        private readonly UserManager<IdentityUser> _userManager;

        public AuthManagerController(UserManager<IdentityUser> userManager)
        {
            _userManager = userManager;
        }

        [HttpPost("CreateNewRole")]
        public async Task AddNewRole(Models.UserRoleDataDto userRole)
        {
            var user = await _userManager.FindByIdAsync(userRole.UserId);
             await _userManager.AddToRoleAsync(user, userRole.Role);
            Console.WriteLine("Test");
        }
    }
}
