using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace InventoryManagementSystem.BLL.Models.CustomTypeGenerators
{
    public class Bit20RandomGenerator : ICustomGenerator
    {
        private static readonly Random _random = new();
        public string PatternValue { get; set; }

        public Bit20RandomGenerator(string pattern)
        {
            this.PatternValue = pattern;
        }

        public string GenerateNew()
        {
            int value = _random.Next(0, 1 << 20);
            return PatternValue + value.ToString();
        }
    }
}
