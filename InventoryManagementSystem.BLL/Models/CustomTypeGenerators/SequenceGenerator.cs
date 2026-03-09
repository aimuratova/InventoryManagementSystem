using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace InventoryManagementSystem.BLL.Models.CustomTypeGenerators
{
    public class SequenceGenerator : ICustomGenerator
    {
        private long _current;
        public string PatternValue { get; set; }

        public SequenceGenerator(string pattern, long current = 0)
        {
            PatternValue = pattern;
            _current = current;
        }

        public string GenerateNew()
        {
            long value = Interlocked.Increment(ref _current);
            return string.Format("{0}{1}", PatternValue, value.ToString());
        }
    }
}
