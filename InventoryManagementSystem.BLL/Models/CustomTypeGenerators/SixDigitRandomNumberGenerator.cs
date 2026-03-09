using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace InventoryManagementSystem.BLL.Models.CustomTypeGenerators
{
    public class SixDigitRandomNumberGenerator : ICustomGenerator
    {
        public string PatternValue { get; set; }
        private static readonly Random _random = new();


        public SixDigitRandomNumberGenerator(string pattern)
        {
            PatternValue = pattern;
        }

        public string GenerateNew()
        {
            var value = _random.Next(100000, 1000000).ToString();
            return string.Format("{0}{1}", PatternValue, value.ToString());
        }
    }
}
