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
    public class InventoryValueService : IInventoryValueService
    {
        private readonly IInventoryValueRepository _valueRepository;
        public InventoryValueService(IInventoryValueRepository valueRepository) 
        {
            this._valueRepository = valueRepository;
        }

        public async Task AddValues(InventoryItemValueModel value)
        {
            await _valueRepository.AddValues(value);
        }

        public async Task<InventoryItemValueModel> GetInventoryValueById(int valueId)
        {
            var result = await _valueRepository.GetInventoryValueById(valueId);
            return result;
        }

        public async Task<DataTable> GetInventoryValueDTById(int inventoryId)
        {
            var resultDt = await _valueRepository.GetInventoryValuesById(inventoryId);

            return resultDt;
        }
    }
}
