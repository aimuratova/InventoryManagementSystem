using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace InventoryManagementSystem.BLL.Models.CustomTypeGenerators
{
    public class NineDigitRandomNumberGenerator : ICustomGenerator
    {
        private static readonly Random _random = new();

        public NineDigitRandomNumberGenerator()
        {
        }

        public string GenerateNew(string? value)
        {
            var valueRandom = _random.Next(100000000, 1000000000).ToString();
            return string.Format("{0}{1}", value ?? string.Empty, valueRandom);
        }
    }
}
