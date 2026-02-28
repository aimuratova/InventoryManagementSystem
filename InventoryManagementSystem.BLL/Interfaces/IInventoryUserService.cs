using InventoryManagementSystem.BLL.Models;
using InventoryManagementSystem.DAL.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace InventoryManagementSystem.BLL.Interfaces
{
    public interface IInventoryUserService
    {
        Task<ResultModel> AddInventoryItemUser(string userId, List<int> inventoryIds);
        Task<List<InventoryItemUserModel>> GetInventoryItemsUserModels(string? userId = null);
        Task<List<string>> GetInventoryUsersByInventoryId(int id);
    }
}
