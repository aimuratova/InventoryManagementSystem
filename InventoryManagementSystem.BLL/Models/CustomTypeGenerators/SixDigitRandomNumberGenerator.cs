using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace InventoryManagementSystem.BLL.Models.CustomTypeGenerators
{
    public class SixDigitRandomNumberGenerator : ICustomGenerator
    {
        private static readonly Random _random = new();

        public SixDigitRandomNumberGenerator()
        {
        }

        public string GenerateNew(string? value)
        {
            var randomVal = _random.Next(100000, 1000000).ToString();
            return string.Format("{0}{1}", value ?? string.Empty, randomVal);
        }
    }
}
