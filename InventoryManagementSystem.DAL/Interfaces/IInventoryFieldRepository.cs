using InventoryManagementSystem.DAL.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace InventoryManagementSystem.DAL.Interfaces
{
    public interface IInventoryFieldRepository
    {
        Task<List<InventoryFieldModel>> GetFieldsByItemIdAsync(int itemId);
    }
}
