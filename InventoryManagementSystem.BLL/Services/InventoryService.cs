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
        public InventoryService(IInventoryRepository inventoryRepository)
        {
            _inventoryRepository = inventoryRepository;
        }

        public async Task<ResultModel<int>> AddInventory(InventoryItemModel inventory)
        {
            var result = new ResultModel<int>();

            if (inventory == null)
            {
                result.Success = false;
                result.Message = "Unable to add empty inventory";
            }
            else if (inventory.CategoryId == 0 || string.IsNullOrEmpty(inventory.InventoryItemTitle) ||
                string.IsNullOrEmpty(inventory.CreatedBy))
            {
                result.Success = false;
                result.Message = "Unable to add inventory, fields not provided";
            }
            else
            {
                try
                {
                    int insertedInvId = await _inventoryRepository.Add(inventory);
                    result.Success = true;
                    result.Data = insertedInvId;
                }
                catch (Exception ex)
                {
                    result.Success = false;
                    result.Message = ex.Message;
                }
            }

            return result;
        }

        public async Task<ResultModel> DeleteInventory(int inventoryId)
        {
            var result = new ResultModel();
            if (inventoryId == 0)
            {
                result.Success = false;
                result.Message = "Id not provided";
            }
            else
            {
                await _inventoryRepository.Delete(inventoryId);
                result.Success = true;
            }

            return result;
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

        public async Task<ResultModel<List<InventoryItemModel>>> GetInventoryItems(string? userId, int? categoryId, string? searchText, string? inventoryType)
        {
            var invItems = await _inventoryRepository.GetInventoryItems(userId, categoryId, searchText,
                !string.IsNullOrEmpty(inventoryType) && inventoryType == "all" ? true : null);
            var result = new ResultModel<List<InventoryItemModel>>
            {
                Success = true,
                Data = invItems
            };
            return result;
        }
        
        public async Task<ResultModel> UpdateInventory(InventoryItemModel inventory, List<string> inventoryUsers, List<InventoryFieldModel> fieldsList)
        {
            var result = new ResultModel();

            if (inventory != null && inventory.InventoryItemId > 0)
            {
                var updateResult = await _inventoryRepository.UpdateInventoryItem(inventory, inventoryUsers, fieldsList);
                if(updateResult.IsUpdated)
                {
                    result.Success = true;
                }
                else
                {
                    result.Success = false;
                    result.Message = updateResult.ErrorMessage;
                }
            }
            else
            {
                result.Success = false;
                result.Message = "Unable to update empty model";
            }
            
            return result;
        }                
    }
}
