using InventoryManagementSystem.BLL.Models;
using InventoryManagementSystem.DAL.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace InventoryManagementSystem.BLL.Interfaces
{
    public interface IDictionaryService
    {
        Task<Dictionary<int, string>> GetInventoriesAsync();
        Task<Dictionary<int, string>> GetCategoriesAsync();
        Task<List<FieldTypeModel>> GetFieldTypesAsync();
    }
}
