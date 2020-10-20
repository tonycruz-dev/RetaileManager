using CDMLibrary.Models;
using System.Collections.Generic;

namespace CDMLibrary.DataAccess
{
    public interface IProductData
    {
        ProductModel GetProductById(int Id);
        List<ProductModel> GetProducts();
    }
}