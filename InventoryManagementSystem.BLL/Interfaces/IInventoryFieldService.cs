using InventoryManagementSystem.BLL.Models;
using InventoryManagementSystem.DAL.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace InventoryManagementSystem.BLL.Interfaces
{
    public interface IInventoryFieldService
    {
        Task<List<InventoryFieldModel>> GetInventoryItemFieldsById(int inventoryId);
    }
}
