namespace InventoryManagementSystem.Models
{
    public class ListQueryParam
    {
        public int? CategoryId { get; set; }
        public string? SearchText { get; set; }
        public string? InventoryType { get; set; }
    }
}
