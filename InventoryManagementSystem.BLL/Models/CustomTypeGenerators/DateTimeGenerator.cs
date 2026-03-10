using Microsoft.SqlServer.Server;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace InventoryManagementSystem.BLL.Models.CustomTypeGenerators
{
    public class DateTimeGenerator : ICustomGenerator
    {
        public DateTimeGenerator() 
        { 
        }

        public string GenerateNew(string? value)
        {
            return DateTime.UtcNow.ToString(value);
        }
    }
}
