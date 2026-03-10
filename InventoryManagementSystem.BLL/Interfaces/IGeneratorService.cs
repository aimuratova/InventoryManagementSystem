using InventoryManagementSystem.BLL.Models.CustomTypeGenerators;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace InventoryManagementSystem.BLL.Interfaces
{
    public interface IGeneratorService
    {
        Task<string> Generate(int typeId, string value);
        ICustomGenerator GetGeneratorType(int customIdType, string value);
    }
}
