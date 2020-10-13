using CDMLibrary.Internal.DataAccess;
using CDMLibrary.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CDMLibrary.DataAccess
{
    public class ProductData
    {
        private const string _connectionStringName = "CDMDataConnection";
       
        public List<ProductModel> GetProducts()
        {

            SqlDataAccess _db = new SqlDataAccess();

            return _db.LoadData<ProductModel, dynamic>
                ("dbo.spProduct_GetAll", new { }, _connectionStringName, true);
        }
    }
}
