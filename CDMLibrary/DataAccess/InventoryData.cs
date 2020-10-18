using CDMLibrary.Internal.DataAccess;
using CDMLibrary.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CDMLibrary.DataAccess
{
    public class InventoryData
    {
        private const string _connectionStringName = "CDMDataConnection";

        public List<InventoryModel> GetInventory()
        {

            SqlDataAccess _db = new SqlDataAccess();

            return _db.LoadData<InventoryModel, dynamic>
                ("dbo.spInventory_GetAll", new { }, _connectionStringName, true);
        }
        public void SaveInventoryRecord(InventoryModel inventory)
        {
            SqlDataAccess _db = new SqlDataAccess();
            _db.SaveData("dbo.spInventory_Insert", inventory, _connectionStringName, true);
        }
    }
}
