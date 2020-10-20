using CDMLibrary.Internal.DataAccess;
using CDMLibrary.Models;
using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CDMLibrary.DataAccess
{
    public class InventoryData : IInventoryData
    {
        private const string _connectionStringName = "CDMDataConnection";
        private readonly ISqlDataAccess _sqlDataAccess;

        public InventoryData(ISqlDataAccess sqlDataAccess)
        {
            _sqlDataAccess = sqlDataAccess;
        }
        public List<InventoryModel> GetInventory()
        {

            return _sqlDataAccess.LoadData<InventoryModel, dynamic>
                ("dbo.spInventory_GetAll", new { }, _connectionStringName, true);
        }
        public void SaveInventoryRecord(InventoryModel inventory)
        {
            _sqlDataAccess.SaveData("dbo.spInventory_Insert", inventory, _connectionStringName, true);
        }
    }
}
