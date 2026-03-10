using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace InventoryManagementSystem.BLL.Models.CustomTypeGenerators
{
    public class GuidGenerator : ICustomGenerator
    {
        public GuidGenerator() 
        { 
        }

        public string GenerateNew(string? value)
        {
            var guidStr = Guid.NewGuid().ToString();
            return string.Format("{0}{1}", value ?? string.Empty, guidStr);
        }
    }
}
