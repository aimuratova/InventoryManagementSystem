using InventoryManagementSystem.BLL.Interfaces;
using InventoryManagementSystem.BLL.Models;
using InventoryManagementSystem.DAL.Interfaces;
using InventoryManagementSystem.DAL.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace InventoryManagementSystem.BLL.Services
{
    public class DictionaryService : IDictionaryService
    {
        private readonly IDictionaryRepository _dictionaryRepository;
        public DictionaryService(IDictionaryRepository dictionaryRepository)
        {
            _dictionaryRepository = dictionaryRepository;
        }

        public async Task<Dictionary<int, string>> GetCategoriesAsync()
        {
            return await _dictionaryRepository.GetCategoriesAsync();
        }

        public async Task<List<InventoryCustomTypeModel>> GetCustomTypesAsync()
        {
            return await _dictionaryRepository.GetCustomTypesAsync();
        }

        public async Task<List<FieldTypeModel>> GetFieldTypesAsync()
        {
            return await _dictionaryRepository.GetFieldTypesAsync();
        }

        public async Task<Dictionary<int, string>> GetInventoriesAsync()
        {
            return await _dictionaryRepository.GetInventoriesAsync();
        }
    }
}
