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

        private readonly ISalesData _salesData;

        public SalesController(ISalesData salesData)
        {
            _salesData = salesData;
        }
        [Authorize(Roles = "Cashier")]
        [HttpPost("AddSale")]
        public void AddSale(SalesModel sale)
        {
            string id = User.FindFirstValue(ClaimTypes.NameIdentifier); // RequestContext.Principal.Identity.GetUserId();
            
            _salesData.SaveSale(sale, id);
        }
        [Authorize(Roles = "Admin,Manager")]
        [HttpGet("GetSalesReport")]
        public List<SaleReportModel> GetSalesReport()
        {
            return _salesData.GetSaleReport();
        }
    }
}
