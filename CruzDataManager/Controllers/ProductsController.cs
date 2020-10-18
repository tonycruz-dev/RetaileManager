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
    [Authorize(Roles = "Cashier,Admin")]
    [RoutePrefix("api/products")]
    public class ProductsController : ApiController
    {

        [HttpGet]
        public List<ProductModel> GetProducts()
        {
            ProductData data = new ProductData();
            return data.GetProducts();
        }
    }
}
