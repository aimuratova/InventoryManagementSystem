using InventoryManagementSystem.DAL.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace InventoryManagementSystem.BLL.Interfaces
{
    public interface IInventoryCustomIdService
    {
        Task<string?> GenerateId(int inventoryId);
        Task<List<InventoryCustomIdValueModel>> GetSelectedCustomType(int inventoryId);
    }
}
