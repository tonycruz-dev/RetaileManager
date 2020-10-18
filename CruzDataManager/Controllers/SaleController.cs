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
        [Authorize(Roles = "Cashier")]
        [Route("AddSale")]
        [HttpPost]
        public void AddSale(SalesModel sale)
        {
            string id = RequestContext.Principal.Identity.GetUserId();
            SalesData data = new SalesData();
            data.SaveSale(sale, id);
        }
        [Authorize(Roles = "Admin,Manager")]
        [Route("GetSalesReport")]
        [HttpGet]
        public List<SaleReportModel> GetSalesReport()
        {
            SalesData data = new SalesData();
            return data.GetSaleReport();
        }
    }
}
