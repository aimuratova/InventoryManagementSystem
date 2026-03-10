using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace InventoryManagementSystem.DAL.Models
{
    public class InventoryCustomIdValueModel
    {
        public int Id { get; set; }
        public int InventoryId { get; set; }
        public int CustomIdType { get; set; }
        public int OrderNum { get; set; }
        public string Value { get; set; }
    }
}
