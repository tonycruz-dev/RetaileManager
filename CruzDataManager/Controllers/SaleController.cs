using CDMLibrary.DataAccess;
using CDMLibrary.Models;
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
    [RoutePrefix("api/Sale")]
    public class SaleController : ApiController
    {
        [Route("AddSale")]
        [HttpPost]
        public void AddSale(SalesModel sale)
        {
            Console.WriteLine();
            string id = RequestContext.Principal.Identity.GetUserId();
            SalesData data = new SalesData();
            data.SaveSale(sale, id);
            //return data.GetUserById(id).First();
        }
    }
}
