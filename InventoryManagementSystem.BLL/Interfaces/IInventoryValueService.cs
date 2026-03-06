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
    public interface IInventoryValueService
    {
        Task<DataTable> GetInventoryValueDTById(int inventoryId);
        Task AddValues(InventoryItemValueModel value);
    }
}
