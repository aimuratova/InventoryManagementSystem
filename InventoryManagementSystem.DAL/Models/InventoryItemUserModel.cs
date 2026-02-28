using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace InventoryManagementSystem.DAL.Models
{
    public class InventoryItemUserModel
    {
        public int InventoryItemId { get; set; }
        public string InventoryItemTitle { get; set; }
        public string UserId { get; set; }
    }
}
