using CDMLibrary.DataAccess;
using CruzDataManager.Models;
using Microsoft.AspNet.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;

namespace CruzDataManager.Controllers
{
    [Authorize]
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
    }
}
