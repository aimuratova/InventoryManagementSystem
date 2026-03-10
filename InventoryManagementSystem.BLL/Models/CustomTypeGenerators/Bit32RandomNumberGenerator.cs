using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace InventoryManagementSystem.BLL.Models.CustomTypeGenerators
{
    public class Bit32RandomNumberGenerator : ICustomGenerator
    {        
        public Bit32RandomNumberGenerator() 
        {
        }

        public string GenerateNew(string? value)
        {
            uint valueInt = BitConverter.ToUInt32(Guid.NewGuid().ToByteArray(), 0);
            return string.Format("{0}{1}", value ?? string.Empty, valueInt);
        }
    }
}
