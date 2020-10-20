using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using CDMLibrary.DataAccess;
using CDMLibrary.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;

namespace CDMApi.Controllers
{
    //[Authorize]
    [Route("api/[controller]")]
    [ApiController]
    public class InventoryController : ControllerBase
    {
        private readonly IConfiguration _config;
        private readonly IInventoryData _inventoryData;

        public InventoryController(IConfiguration config, IInventoryData inventoryData)
        {
            _config = config;
            _inventoryData = inventoryData;
        }
        [Authorize(Roles = "Admin,Manager")]
        [HttpPost("AddInventory")]
        public void AddInventory(InventoryModel inventory)
        {
            //string id = RequestContext.Principal.Identity.GetUserId();
            //InventoryData data = new InventoryData(_config);
            _inventoryData.SaveInventoryRecord(inventory);
        }

       // [Authorize(Roles = "Admin")]
        [Route("GetInventory")]
        [HttpGet]
        public List<InventoryModel> GetSalesReport()
        {
            //InventoryData data = new InventoryData(_config);
            return _inventoryData.GetInventory();
        }
    }
}
