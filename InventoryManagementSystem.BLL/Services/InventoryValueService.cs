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

        public Task AddValues(List<InventoryItemValueModel> valuesList)
        {
            throw new NotImplementedException();
        }

        public async Task<DataTable> GetInventoryValueDTById(int inventoryId)
        {
            var resultDt = await _valueRepository.GetInventoryValuesById(inventoryId);

            return resultDt;
        }
    }
}
