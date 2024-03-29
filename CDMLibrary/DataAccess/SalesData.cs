﻿using CDMLibrary.Internal.DataAccess;
using CDMLibrary.Models;
using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CDMLibrary.DataAccess
{
    public class SalesData : ISalesData
    {
        private const string _connectionStringName = "CDMDataConnection";
        private readonly IConfiguration _config;
        private readonly IProductData _productData;
        private readonly ISqlDataAccess _db;

        public SalesData(IConfiguration config, IProductData productData, ISqlDataAccess db)
        {
            _config = config;
            _productData = productData;
            _db = db;
        }
        public SalesModel SaveSale(SalesModel saleInfo, string userId)
        {

            List<SaleDetailDBModel> details = new List<SaleDetailDBModel>();
            decimal taxRate = decimal.Parse(_config["taxRate"]);

            foreach (var item in saleInfo.SaleDetails)
            {
                var detail = new SaleDetailDBModel
                {
                    ProductId = item.ProductId,
                    Quantity = item.Quantity
                };
                details.Add(detail);
                var productInfo = _productData.GetProductById(detail.ProductId);

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
                CashierId = userId
            };
            sale.SaleDate = DateTime.Now;
            sale.Total = sale.SubTotal + sale.Total;

            //using (SqlDataAccess _db = new SqlDataAccess(_config))
            //{
                try
                {
                    _db.StartTransaction(_connectionStringName);
                    var salesId = _db.SaveDataAndReturnIdInTransaction("dbo.spSale_Insert", sale);

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
        public List<SaleReportModel> GetSaleReport()
        {

           return _db.LoadData<SaleReportModel, dynamic>
                ("dbo.spSales_report", new { }, _connectionStringName, true);

        }
    }
}
