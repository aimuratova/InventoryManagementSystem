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
    public class InventoryFieldService : IInventoryFieldService
    {
        private readonly IInventoryFieldRepository _inventoryFieldRepository;

        public InventoryFieldService(IInventoryFieldRepository inventoryFieldRepository)
        {
            _inventoryFieldRepository = inventoryFieldRepository;
        }

        public async Task<List<InventoryFieldModel>> GetInventoryItemFieldsById(int inventoryId)
        {
            var fieldResultList = await _inventoryFieldRepository.GetFieldsByItemIdAsync(inventoryId);            
            return fieldResultList;
        }
    }
}
