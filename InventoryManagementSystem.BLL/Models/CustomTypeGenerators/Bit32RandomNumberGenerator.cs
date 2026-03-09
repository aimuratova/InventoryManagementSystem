using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace InventoryManagementSystem.BLL.Models.CustomTypeGenerators
{
    public class Bit32RandomNumberGenerator : ICustomGenerator
    {
        public string PatternValue { get; set; }
        
        public Bit32RandomNumberGenerator(string pattern) 
        {
            PatternValue = pattern;
        }

        public string GenerateNew()
        {
            uint value = BitConverter.ToUInt32(Guid.NewGuid().ToByteArray(), 0);
            return string.Format("{0}{1}", PatternValue, value.ToString());
        }
    }
}
