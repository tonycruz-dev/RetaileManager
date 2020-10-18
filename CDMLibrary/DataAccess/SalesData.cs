using CDMLibrary.Internal.DataAccess;
using CDMLibrary.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CDMLibrary.DataAccess
{
    public class SalesData
    {
        private const string _connectionStringName = "CDMDataConnection";

        public SalesModel SaveSale(SalesModel saleInfo, string userId)
        {
            ProductData products = new ProductData();
            List<SaleDetailDBModel> details = new List<SaleDetailDBModel>();
            decimal taxRate = ConfigHelper.GetTax() / 100;
            foreach (var item in saleInfo.SaleDetails)
            {
                var detail = new SaleDetailDBModel
                {
                    ProductId = item.ProductId,
                    Quantity = item.Quantity
                };
                details.Add(detail);
                var productInfo = products.GetProductById(detail.ProductId);

                if (productInfo == null)
                {
                    throw new Exception($"This product id of {detail.ProductId} could not be found in the database!");
                }
                detail.PurchasePrice = (productInfo.RetailPrice * detail.Quantity);

                if (productInfo.IsTaxable)
                {
                    detail.Tax = (detail.PurchasePrice * taxRate);
                }

            }
            SaleDBModel sale = new SaleDBModel()
            {
                SubTotal = details.Sum(x => x.PurchasePrice),
                Tax = details.Sum(x => x.Tax),
                CashierId =  userId
            };
            sale.SaleDate = DateTime.Now;
            sale.Total = sale.SubTotal + sale.Total;

            using (SqlDataAccess _db = new SqlDataAccess())
            {
                try
                {
                    _db.StartTransaction(_connectionStringName);
                    var salesId =  _db.SaveDataAndReturnIdInTransaction("dbo.spSale_Insert", sale);

                    foreach (var item in details)
                    {
                        item.SaleId = salesId;
                        _db.SaveDataInTransaction("dbo.spSaleDetail_Insert", item);
                    }
                   _db.CommitTransaction();
                    return saleInfo;
                }
                catch 
                {

                    _db.RollbackTransaction();
                    return saleInfo;
                }
               
            }
            //var salesId = _db.SaveDataWithId("dbo.spSale_Insert", sale, _connectionStringName, true);

            //foreach (var item in details)
            //{
            //    item.SaleId = salesId;
            //    _db.SaveData("dbo.spSaleDetail_Insert", item, _connectionStringName, true);
            //}
            //return saleInfo;
        }
        public List<SaleReportModel> GetSaleReport()
        {

          SqlDataAccess _db = new SqlDataAccess();

                return _db.LoadData<SaleReportModel, dynamic>
                    ("dbo.spSales_report", new { }, _connectionStringName, true);

        }
    }
}
