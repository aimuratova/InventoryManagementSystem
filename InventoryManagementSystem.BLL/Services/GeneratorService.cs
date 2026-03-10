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
            PopulateListGenerators();
        }

        public async Task<string> Generate(int typeId, string value)
        {
            ICustomGenerator? generator = GetCustomGenerator(typeId);
            if (generator == null)
            {
                // Возвращаем пустую строку, чтобы соответствовать Task<string> и избежать null
                return string.Empty;
            }

            return generator.GenerateNew(value);
        }

        private void PopulateListGenerators()
        {
            listGenerators.Add((int)CustomTypeEnum.FixedText, new FixedTextGenerator());
            listGenerators.Add((int)CustomTypeEnum.Bit20RandomNumber, new Bit20RandomGenerator());
            listGenerators.Add((int)CustomTypeEnum.Bit32RandomNumber, new Bit32RandomNumberGenerator());
            listGenerators.Add((int)CustomTypeEnum.SixDigitRandomNumber, new SixDigitRandomNumberGenerator());
            listGenerators.Add((int)CustomTypeEnum.NineDigitRandomNumber, new NineDigitRandomNumberGenerator());
            listGenerators.Add((int)CustomTypeEnum.Guid, new GuidGenerator());
            listGenerators.Add((int)CustomTypeEnum.DateTime, new DateTimeGenerator());
            listGenerators.Add((int)CustomTypeEnum.Sequence, new SequenceGenerator());
        }

        public ICustomGenerator? GetCustomGenerator(int typeId)
        {            
            if (listGenerators.ContainsKey(typeId))
            {
                return listGenerators[typeId];
            }
            return null;
        }
    }
}
