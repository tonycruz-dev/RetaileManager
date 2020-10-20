using CDMLibrary.Internal.DataAccess;
using CDMLibrary.Models;
using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace CDMLibrary.DataAccess
{
    public class ProductData
    {
        private const string _connectionStringName =  "CDMDataConnection";
        private readonly IConfiguration _config;

        public ProductData(IConfiguration config)
        {
            _config = config;
        }  
        public List<ProductModel> GetProducts()
        {

            SqlDataAccess _db = new SqlDataAccess(_config);

            return _db.LoadData<ProductModel, dynamic>
                ("dbo.spProduct_GetAll", new { }, _connectionStringName, true);
        }
        public ProductModel GetProductById(int Id)
        {

            SqlDataAccess _db = new SqlDataAccess(_config);

            return _db.LoadData<ProductModel, dynamic>
                ("dbo.spProduct_ById", new { Id}, _connectionStringName, true).SingleOrDefault();
        }
    
    }
}
