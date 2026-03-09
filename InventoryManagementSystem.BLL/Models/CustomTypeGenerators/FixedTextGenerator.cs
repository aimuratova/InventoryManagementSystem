using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace InventoryManagementSystem.BLL.Models.CustomTypeGenerators
{
    public class FixedTextGenerator : ICustomGenerator
    {
        public string PatternValue { get ; set; }

        public FixedTextGenerator(string pattern)
        {
            PatternValue = pattern;            
        }

        public string GenerateNew()
        {
            return PatternValue;
        }
    }
}
