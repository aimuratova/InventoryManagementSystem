using InventoryManagementSystem.DAL.Models;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace InventoryManagementSystem.DAL.Interfaces
{
    public interface IInventoryValueRepository
    {
        Task<DataTable> GetInventoryValuesById(int inventoryId);
        Task AddValues(InventoryItemValueModel value);
        Task<InventoryItemValueModel> GetInventoryValueById(int valueId);
        Task Delete(int valueId);
        Task<ResultDbModel> Update(InventoryItemValueModel updateModel);
    }
}
