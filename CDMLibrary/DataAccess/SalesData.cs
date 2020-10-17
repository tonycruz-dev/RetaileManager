﻿using CDMLibrary.Internal.DataAccess;
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

             SqlDataAccess _db = new SqlDataAccess();
           var salesId =  _db.SaveDataWithId("dbo.spSale_Insert", sale, _connectionStringName, true);

            foreach (var item in details)
            {
                item.SaleId = salesId;
                _db.SaveData("dbo.spSaleDetail_Insert", item, _connectionStringName, true);
            }
            return saleInfo;
        }
    }
}