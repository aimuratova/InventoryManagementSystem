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
        Task<UsersModel?> GetByEmail(string email);
        Task<UsersModel?> GetUserById(string userId);
        Task<List<UsersModel>> GetUsers();
        Task UpdateAsync(UsersModel user);
    }
}
