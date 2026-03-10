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
        
        public Bit20RandomGenerator()
        {
        }


        public string GenerateNew(string? value)
        {
            int valueInt = _random.Next(0, 1 << 20);
            return string.Format("{0}{1}", value ?? string.Empty, valueInt);
        }
    }
}
