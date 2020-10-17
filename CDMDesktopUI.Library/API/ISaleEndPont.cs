using CDMLibrary.Models;
using System.Threading.Tasks;

namespace CDMDesktopUI.Library.API
{
    public interface ISaleEndPont
    {
        Task<SalesModel> AddSales(SalesModel sales);
    }
}