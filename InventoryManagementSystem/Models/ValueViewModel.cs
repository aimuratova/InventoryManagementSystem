namespace InventoryManagementSystem.Models
{
    public class ValueViewModel
    {
        public int Id { get; set; }
        public int InventoryId { get; set; }
        public int FieldId { get; set; }
        public int TypeId { get; set; }
        public object Value { get; set; }
        public int RowNum { get; set; }
    }
}
