using InventoryManagementSystem.BLL.Interfaces;
using InventoryManagementSystem.BLL.Models;
using InventoryManagementSystem.DAL.Interfaces;
using InventoryManagementSystem.DAL.Models;
using InventoryManagementSystem.DAL.Repositories;
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

        public async Task<ResultModel> Delete(int valueId)
        {
            var result = new ResultModel();
            if (valueId == 0)
            {
                result.Success = false;
                result.Message = "Id not provided";
            }
            else
            {
                await _valueRepository.Delete(valueId);
                result.Success = true;
            }

            return result;
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

        public async Task<ResultModel> Update(InventoryItemValueModel updateModel)
        {
            var result = new ResultModel();

            if (updateModel != null && updateModel.Id > 0)
            {
                var updateResult = await _valueRepository.Update(updateModel);
                if (updateResult.IsUpdated)
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
