using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace InventoryManagementSystem.DAL.Models
{
    public class InventoryItemValueModel
    {
        public int Id { get; set; }
        public int FieldId { get; set; }
        public int FieldValue { get; set; }
        public int RowId { get; set; }
    }
}
