using InventoryManagementSystem.BLL.Models;
using InventoryManagementSystem.DAL.Models;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace InventoryManagementSystem.BLL.Interfaces
{
    public interface IInventoryService
    {        
        Task<ResultModel<List<InventoryItemModel>>> GetInventoryItems();
        Task<ResultModel<DataSet>> GetInventoryValues();
        Task<InventoryItemModel> GetInventoryItemById(int id);
        Task<ResultModel> UpdateInventory(InventoryItemModel inventory, List<string> inventoryUsers,
            List<InventoryFieldModel> fieldsList);
        Task<ResultModel<int>> AddInventory(InventoryItemModel inventory);
        Task<ResultModel> DeleteInventory(int inventoryId);
    }
}
