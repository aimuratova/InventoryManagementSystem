using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace InventoryManagementSystem.DAL.Interfaces
{
    public interface IDictionaryRepository
    {
        Task<Dictionary<int, string>> GetCategoriesAsync();
        Task<Dictionary<int, string>> GetInventoriesAsync();
        Task<Dictionary<int, string>> GetRolesAsync(string filter);
    }
}
