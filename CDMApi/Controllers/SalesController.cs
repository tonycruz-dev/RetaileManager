using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;
using CDMLibrary.DataAccess;
using CDMLibrary.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;

namespace CDMApi.Controllers
{

    [Authorize]
    [Route("api/[controller]")]
    public class SalesController : ControllerBase
    {
        private readonly IConfiguration _config;

        public SalesController(IConfiguration config)
        {
            _config = config;
        }
        [Authorize(Roles = "Cashier")]
        [HttpPost("AddSale")]
        public void AddSale(SalesModel sale)
        {
            string id = User.FindFirstValue(ClaimTypes.NameIdentifier); // RequestContext.Principal.Identity.GetUserId();
            SalesData data = new SalesData(_config);
            data.SaveSale(sale, id);
        }
        [Authorize(Roles = "Admin,Manager")]
        [HttpGet("GetSalesReport")]
        public List<SaleReportModel> GetSalesReport()
        {
            SalesData data = new SalesData(_config);
            return data.GetSaleReport();
        }
    }
}
