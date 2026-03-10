using InventoryManagementSystem.DAL.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace InventoryManagementSystem.DAL.Interfaces
{
    public interface IInventoryCustomIdRepository
    {
        Task<List<InventoryCustomIdValueModel>> GetAllByInventoryId(int inventoryId);
    }
}
