using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace InventoryManagementSystem.BLL.Models.CustomTypeGenerators
{
    public class GuidGenerator : ICustomGenerator
    {
        public string PatternValue { get; set; }

        public GuidGenerator(string value) 
        { 
            PatternValue = value; 
        }

        public string GenerateNew()
        {
            var value = Guid.NewGuid().ToString();
            return string.Format("{0}{1}", PatternValue, value.ToString());
        }
    }
}
