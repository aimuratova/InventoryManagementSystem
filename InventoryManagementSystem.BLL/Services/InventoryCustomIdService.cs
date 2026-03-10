using InventoryManagementSystem.BLL.Interfaces;
using InventoryManagementSystem.DAL.Interfaces;
using InventoryManagementSystem.DAL.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace InventoryManagementSystem.BLL.Services
{
    public class InventoryCustomIdService : IInventoryCustomIdService
    {
        private readonly IInventoryCustomIdRepository _repository;

        public InventoryCustomIdService(IInventoryCustomIdRepository repository)
        {
            _repository = repository;
        }

        public async Task<List<InventoryCustomIdValueModel>> GetSelectedCustomType(int inventoryId)
        {
            return await _repository.GetAllByInventoryId(inventoryId);
        }
    }
}
