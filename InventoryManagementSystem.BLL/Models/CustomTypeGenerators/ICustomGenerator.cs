using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace InventoryManagementSystem.BLL.Models.CustomTypeGenerators
{
    public interface ICustomGenerator
    {
        public string PatternValue { get; set; }
        string GenerateNew();
    }
}
