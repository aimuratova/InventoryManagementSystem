using InventoryManagementSystem.BLL.Interfaces;
using InventoryManagementSystem.BLL.Models;
using InventoryManagementSystem.DAL.Interfaces;
using InventoryManagementSystem.DAL.Models;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace InventoryManagementSystem.BLL.Services
{
    public class InventoryService : IInventoryService
    {
        private readonly IInventoryRepository _inventoryRepository;
        private readonly IInventoryUserRepository _inventoryUserRepository;

        public InventoryService(IInventoryRepository inventoryRepository, IInventoryUserRepository inventoryUserRepository)
        {
            _inventoryRepository = inventoryRepository;
            _inventoryUserRepository = inventoryUserRepository;
        }
                
        public async Task<InventoryItemModel> GetInventoryItemById(int id)
        {
            var item = await _inventoryRepository.GetInventoryItemById(id);
            if (item == null)
            {
                throw new InvalidOperationException($"Inventory item with id {id} not found.");
            }
            return item;
        }

        public async Task<ResultModel<List<InventoryItemModel>>> GetInventoryItems()
        {
            var invItems = await _inventoryRepository.GetInventoryItems();
            var result = new ResultModel<List<InventoryItemModel>>
            {
                Success = true,
                Data = invItems
            };
            return result;
        }
               
        public async Task<ResultModel<DataSet>> GetInventoryValues()
        {
            var dataTables = await _inventoryRepository.GetInventoryValues();
            var result = new ResultModel<DataSet>
            {
                Success = true,
                Data = dataTables
            };
            return result;
        }


    }
}
