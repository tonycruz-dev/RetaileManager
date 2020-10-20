using CDMLibrary.DataAccess;
using CDMLibrary.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;

namespace CDMApi.Controllers
{
    //[Authorize(Roles = "Cashier")]
    [Route("api/[controller]")]
    [ApiController]
    public class ProductsController : ControllerBase
    {
        //private readonly IConfiguration _config;
        private readonly IProductData _productData;

        public ProductsController(IProductData productData)
        {
            _productData = productData;
        }
        [HttpGet("GetProducts")]
        public List<ProductModel> GetProducts()
        {
            return _productData.GetProducts();
        }
    }
}
