using CDMLibrary.DataAccess;
using CDMLibrary.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;

namespace CruzDataManager.Controllers
{
    [Authorize]
    [RoutePrefix("api/Inventory")]
    public class InventoryController : ApiController
    {
        [Authorize(Roles = "Admin,Manager")]
        [Route("AddInventory")]
        [HttpPost]
        public void AddInventory(InventoryModel inventory)
        {
            //string id = RequestContext.Principal.Identity.GetUserId();
            InventoryData data = new InventoryData();
            data.SaveInventoryRecord(inventory);
        }

        [Authorize(Roles = "Admin")]
        [Route("GetInventory")]
        [HttpGet]
        public List<InventoryModel> GetSalesReport()
        {
            InventoryData data = new InventoryData();
            return data.GetInventory();
        }
    }
}
