using InventoryManagementSystem.DAL.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace InventoryManagementSystem.DAL.Interfaces
{
    public interface IUserRepository
    {
        Task Add(UsersModel user);
        Task<bool> GetAccessInventory(string? userId, int inventoryId);
        Task<UsersModel?> GetByEmail(string email);
        Task<UsersModel?> GetUserById(string userId);
        Task<List<UsersModel>> GetUsers();
        Task<bool> IsInRoleAdmin(string? userId);
        Task UpdateAsync(UsersModel user);
    }
}
