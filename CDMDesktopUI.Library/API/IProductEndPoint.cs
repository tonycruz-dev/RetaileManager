using CDMDesktopUI.Library.Models;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace CDMDesktopUI.Library.API
{
    public interface IProductEndPoint
    {
        Task<List<ProductModel>> GetAll();
    }
}