using InventoryManagementSystem.DAL.Models;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace InventoryManagementSystem.DAL.Interfaces
{
    public interface IInventoryRepository
    {
        Task<List<InventoryItemModel>> GetInventoryItems();
        Task<DataSet> GetInventoryValues();
        Task<InventoryItemModel?> GetInventoryItemById(int inventoryId);
        Task<ResultDbModel> UpdateInventoryItem(InventoryItemModel inventoryModel, List<string> inventoryUsers, 
            List<InventoryFieldModel> fieldsList);

    }
}
