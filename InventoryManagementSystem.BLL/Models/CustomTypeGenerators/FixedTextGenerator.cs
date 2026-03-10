using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace InventoryManagementSystem.BLL.Models.CustomTypeGenerators
{
    public class FixedTextGenerator : ICustomGenerator
    {
        public FixedTextGenerator()
        {
        }

        public string GenerateNew(string? value)
        {
            return value ?? string.Empty;
        }
    }
}
