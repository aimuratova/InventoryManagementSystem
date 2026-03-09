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
        public string PatternValue { get; set; }
        public DateTimeGenerator(string pattern = "yyyyMMddHHmmss") 
        { 
            PatternValue = pattern;
        }

        public string GenerateNew()
        {
            return DateTime.UtcNow.ToString(PatternValue);
        }
    }
}
