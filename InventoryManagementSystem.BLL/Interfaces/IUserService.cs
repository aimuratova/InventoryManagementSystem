using InventoryManagementSystem.BLL.Models;
using InventoryManagementSystem.DAL.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace InventoryManagementSystem.BLL.Interfaces
{
    public interface IUserService
    {
        Task<ResultModel> CreateAsync(string email, string password);
        Task<ResultModel<string>> AuthenticateAsync(string email, string password);
        Task<ResultModel<UsersModel>> GetUserById(string userId);
        Task<ResultModel<List<UsersModel>>> ListUsers();        
        Task<ResultModel> UpdateUserAsync(UsersModel user);
        Task<bool> GetAccessInventory(string? userId, int inventoryId);
    }
}
