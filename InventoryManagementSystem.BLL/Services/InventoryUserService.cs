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
    public class InventoryUserService : IInventoryUserService
    {
        private readonly IInventoryUserRepository _inventoryUserRepository;

        public InventoryUserService(IInventoryUserRepository inventoryUserRepository)
        {
            _inventoryUserRepository = inventoryUserRepository;
        }

        public async Task<ResultModel> AddInventoryItemUser(string userId, List<int> inventoryIds)
        {
            var result = new ResultModel();
            try
            {
                if (inventoryIds != null && inventoryIds.Any())
                {
                    await _inventoryUserRepository.AddInventoryItemUserId(userId, inventoryIds);
                }
                else
                {
                    await _inventoryUserRepository.RemoveInventoryItemUserId(userId);
                }
                result.Success = true;
                result.Message = "Inventory items added successfully.";
            }
            catch (Exception ex)
            {
                result.Success = false;
                result.Message = "An error occurred while adding inventory items.";
                result.Errors = new List<string> { ex.Message };
            }

            return result;
        }


        public async Task<List<InventoryItemUserModel>> GetInventoryItemsUserModels(string? userId = null)
        {
            var inventoryItemUserModels = await _inventoryUserRepository.GetInventoryItemsUserModels(userId);
            return inventoryItemUserModels;
        }

        public async Task<List<string>> GetInventoryUsersByInventoryId(int id)
        {
            var usersList = await _inventoryUserRepository.GetInventoryUsersByInventoryId(id);
            return usersList;
        }
    }
}
