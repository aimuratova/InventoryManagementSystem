using InventoryManagementSystem.BLL.Interfaces;
using InventoryManagementSystem.BLL.Models;
using InventoryManagementSystem.BLL.Models.CustomTypeGenerators;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace InventoryManagementSystem.BLL.Services
{
    public class GeneratorService : IGeneratorService
    {
        private Dictionary<int, ICustomGenerator> listGenerators;

        public GeneratorService()
        {
            listGenerators = new Dictionary<int, ICustomGenerator>();
        }

        public Task<string> Generate(int typeId, string value)
        {
            if (listGenerators.Count == 0)
            {
                PopulateListGenerators(value);
            }

            ICustomGenerator? generator = GetCustomGenerator(typeId);
            if (generator == null)
            {
                // Возвращаем пустую строку, чтобы соответствовать Task<string> и избежать null
                return Task.FromResult(string.Empty);
            }

            string? result = generator.GenerateNew();
            // Гарантируем, что возвращаемое значение не null
            return Task.FromResult(result ?? string.Empty);
        }

        private void PopulateListGenerators(string value)
        {
            listGenerators.Add((int)CustomTypeEnum.FixedText, new FixedTextGenerator(value));
            listGenerators.Add((int)CustomTypeEnum.Bit20RandomNumber, new Bit20RandomGenerator(value));
            listGenerators.Add((int)CustomTypeEnum.Bit32RandomNumber, new Bit32RandomNumberGenerator(value));
            listGenerators.Add((int)CustomTypeEnum.SixDigitRandomNumber, new SixDigitRandomNumberGenerator(value));
            listGenerators.Add((int)CustomTypeEnum.NineDigitRandomNumber, new NineDigitRandomNumberGenerator(value));
            listGenerators.Add((int)CustomTypeEnum.Guid, new GuidGenerator(value));
            listGenerators.Add((int)CustomTypeEnum.DateTime, new DateTimeGenerator(value));
            listGenerators.Add((int)CustomTypeEnum.Sequence, new SequenceGenerator(value));
        }

        private ICustomGenerator? GetCustomGenerator(int typeId)
        {            
            if (listGenerators.ContainsKey(typeId))
            {
                return listGenerators[typeId];
            }
            return null;
        }

        public ICustomGenerator GetGeneratorType(int customIdType, string value)
        {
            listGenerators.Clear();

            PopulateListGenerators(value);            
            return listGenerators[customIdType];
        }
    }
}
