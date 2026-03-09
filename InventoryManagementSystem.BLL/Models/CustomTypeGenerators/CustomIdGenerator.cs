using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace InventoryManagementSystem.BLL.Models.CustomTypeGenerators
{
    public class CustomIdGenerator
    {
        private readonly List<ICustomGenerator> _generators;

        public CustomIdGenerator(params ICustomGenerator[] generators)
        {
            _generators = generators.ToList();
        }

        public string GenerateId()
        {
            var sb = new StringBuilder();

            foreach (var component in _generators)
            {
                sb.Append(component.GenerateNew());
            }

            return sb.ToString();
        }
    }
}
