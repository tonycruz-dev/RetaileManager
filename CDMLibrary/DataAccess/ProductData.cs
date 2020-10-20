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
    public class ProductData : IProductData
    {
        private const string _connectionStringName = "CDMDataConnection";
         private readonly ISqlDataAccess _db;

        public ProductData(ISqlDataAccess db)
        {
        
            _db = db;
        }
        public List<ProductModel> GetProducts()
        {

            return _db.LoadData<ProductModel, dynamic>
                ("dbo.spProduct_GetAll", new { }, _connectionStringName, true);
        }
        public ProductModel GetProductById(int Id)
        {
            return _db.LoadData<ProductModel, dynamic>
                ("dbo.spProduct_ById", new { Id }, _connectionStringName, true).SingleOrDefault();
        }

    }
}
