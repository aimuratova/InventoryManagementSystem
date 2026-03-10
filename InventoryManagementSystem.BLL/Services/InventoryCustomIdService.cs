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
        private CustomIdGenerator? _generator;
        private readonly IGeneratorService _generatorService;

        public InventoryCustomIdService(IInventoryCustomIdRepository repository, IGeneratorService generatorService)
        {
            _repository = repository;
            _generatorService = generatorService;
        }

        public async Task<string?> GenerateId(int inventoryId)
        {
            var customSpecs = await GetSelectedCustomType(inventoryId);
            var customGenerators = new List<ICustomGenerator>();

            foreach (var customType in customSpecs)
            {
                customGenerators.Add(_generatorService.GetGeneratorType(customType.CustomIdType, customType.Value));
            }

            if (_generator == null)
            {
                _generator = new CustomIdGenerator(customGenerators.ToArray());
            }

            return _generator.GenerateId();
        }

        public async Task<List<InventoryCustomIdValueModel>> GetSelectedCustomType(int inventoryId)
        {
            return await _repository.GetAllByInventoryId(inventoryId);
        }
    }
}
