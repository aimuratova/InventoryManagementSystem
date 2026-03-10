using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace InventoryManagementSystem.BLL.Models.CustomTypeGenerators
{
    public class SequenceGenerator : ICustomGenerator
    {
        private long _current = 0;

        public SequenceGenerator()
        {
        }

        public string GenerateNew(string? value)
        {
            long sequence = Interlocked.Increment(ref _current);
            return string.Format("{0}{1}", value ?? string.Empty, sequence);
        }
    }
}
