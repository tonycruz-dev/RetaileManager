using System.Collections.Generic;

namespace CDMLibrary.Models
{
    public class SalesModel
    {
        public List<SaleDetailModel> SaleDetails { get; set; } = new List<SaleDetailModel>();
    }
}
