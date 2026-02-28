using InventoryManagementSystem.BLL.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace InventoryManagementSystem.BLL.Interfaces
{
    public interface IRoleService
    {
        Task<ResultModel> AddUserToRoleAdminAsync(string userId, bool isAdmin);
    }
}
