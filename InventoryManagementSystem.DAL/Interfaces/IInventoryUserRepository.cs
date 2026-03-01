using InventoryManagementSystem.DAL.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace InventoryManagementSystem.DAL.Interfaces
{
    public interface IInventoryUserRepository
    {
        Task<List<string>> GetInventoryUsersByInventoryId(int inventoryId);
        Task AddInventoryItemUserId(string userId, List<int> inventoryItems);
        Task<List<InventoryItemUserModel>> GetInventoryItemsUserModels(string? userId = null);
        Task RemoveInventoryItemUserId(string userId);
        //Task AddInventoryUserInventoryId(List<string> userList, int inventoryId);
        //Task RemoveInventoryUserInventoryId(int inventoryId);
    }
}
