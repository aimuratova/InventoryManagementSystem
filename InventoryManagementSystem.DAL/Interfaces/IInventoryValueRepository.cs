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
    }
}
