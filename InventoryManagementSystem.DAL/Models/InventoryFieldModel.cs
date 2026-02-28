using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace InventoryManagementSystem.DAL.Models
{
    public class InventoryFieldModel
    {
        public int Id { get; set; }
        public int ItemId { get; set; }
        public int TypeId { get; set; }
        public string TypeTitle { get; set; }
        public string DataType { get; set; }
        public string Title { get; set; }
        public string Description { get; set; }
        public bool IsDisplayed { get; set; }
        public int OrderNum { get; set; }
    }
}
