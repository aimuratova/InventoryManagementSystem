using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace InventoryManagementSystem.DAL.Models
{
    public class InventoryItemModel
    {
        public int InventoryItemId { get; set; }
        public string InventoryItemTitle { get; set; }
        public string Description { get; set; }
        public int CategoryId { get; set; }
        public string CategoryTitle { get; set; }
        public string CreatedBy { get; set; }
        public string CreatedByEmail { get; set; }
        public DateTime CreatedAt { get; set; }
        public bool IsPublic { get; set; }
    }
}
