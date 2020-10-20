using CDMLibrary.Models;
using System.Collections.Generic;

namespace CDMLibrary.DataAccess
{
    public interface IInventoryData
    {
        List<InventoryModel> GetInventory();
        void SaveInventoryRecord(InventoryModel inventory);
    }
}