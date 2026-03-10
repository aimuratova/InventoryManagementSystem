using InventoryManagementSystem.BLL.Interfaces;
using InventoryManagementSystem.BLL.Models.CustomTypeGenerators;
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
        private readonly IGeneratorService _generatorService;

        public InventoryCustomIdService(IInventoryCustomIdRepository repository, IGeneratorService generatorService)
        {
            _repository = repository;
            _generatorService = generatorService;
        }

        public async Task<string?> GenerateId(int inventoryId)
        {
            var customSpecs = await GetSelectedCustomType(inventoryId);
            
            var sb = new StringBuilder();

            foreach (var customType in customSpecs)
            {
                var generator = _generatorService.GetCustomGenerator(customType.CustomIdType);
                if (generator != null)
                {
                    sb.Append(generator.GenerateNew(customType.Value));
                }
            }

            return sb.ToString();
        }

        public async Task<List<InventoryCustomIdValueModel>> GetSelectedCustomType(int inventoryId)
        {
            return await _repository.GetAllByInventoryId(inventoryId);
        }
    }
}
