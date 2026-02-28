using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace InventoryManagementSystem.DAL.Interfaces
{
    public interface IRoleRepository
    {
        Task AddUserAdminRole(string userId, int roleId);
        Task RemoveUserAdminRole(string userId, int roleId);
    }
}
