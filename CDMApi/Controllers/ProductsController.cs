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
    [Authorize(Roles = "Cashier")]
    [Route("api/[controller]")]
    [ApiController]
    public class ProductsController : ControllerBase
    {
        private readonly IConfiguration _config;

        public ProductsController(IConfiguration config)
        {
            _config = config;
        }
        [HttpGet("GetProducts")]
        public List<ProductModel> GetProducts()
        {
            ProductData data = new ProductData(_config);
            return data.GetProducts();
        }
    }
}
