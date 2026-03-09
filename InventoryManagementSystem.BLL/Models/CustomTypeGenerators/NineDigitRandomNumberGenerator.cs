using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace InventoryManagementSystem.BLL.Models.CustomTypeGenerators
{
    public class NineDigitRandomNumberGenerator : ICustomGenerator
    {
        public string PatternValue { get; set; }
        private static readonly Random _random = new();

        public NineDigitRandomNumberGenerator(string pattern)
        {
            PatternValue = pattern;
        }

        public string GenerateNew()
        {
            var value = _random.Next(100000000, 1000000000).ToString();
            return string.Format("{0}{1}", PatternValue, value.ToString());
        }
    }
}
