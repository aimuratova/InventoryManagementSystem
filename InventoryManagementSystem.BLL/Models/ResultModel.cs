using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace InventoryManagementSystem.BLL.Models
{
    public class ResultModel
    {
        public bool Success { get; set; }
        public string? Message { get; set; }
        public IEnumerable<string> Errors { get; set; } = Enumerable.Empty<string>();
    }

    public class ResultModel<T> : ResultModel
    {
        public T? Data { get; set; }
    }
}
