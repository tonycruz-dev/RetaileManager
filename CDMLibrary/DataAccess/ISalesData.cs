using CDMLibrary.Models;
using System.Collections.Generic;

namespace CDMLibrary.DataAccess
{
    public interface ISalesData
    {
        List<SaleReportModel> GetSaleReport();
        SalesModel SaveSale(SalesModel saleInfo, string userId);
    }
}