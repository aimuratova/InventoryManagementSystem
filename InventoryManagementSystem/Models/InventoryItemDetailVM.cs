using System.Data;

namespace InventoryManagementSystem.Models
{
    public class InventoryItemDetailVM
    {
        public InventoryItemViewModel BasicInfo { get; set; }
        public DataTable ValuesDT { get; set; }
        public List<FieldVM>? Fields { get; set; }        
    }

    public class FieldVM
    {
        public int Id { get; set; }
        public int TypeId { get; set; }
        public string TypeTitle { get; set; }
        public string DataType { get; set; }
        public string Title { get; set; }
        public string Description { get; set; }
        public bool IsDisplayed { get; set; }
        public int OrderNum { get; set; }
    }
}
