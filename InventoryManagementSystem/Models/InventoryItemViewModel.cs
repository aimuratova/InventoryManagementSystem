using System.Data;

namespace InventoryManagementSystem.Models
{
    public class InventoryItemViewModel
    {
        public int Id { get; set; }
        public int Number { get; set; }
        public string Title { get; set; }
        public string Description { get; set; }
        public string CreatedBy { get; set; }
        public DateTime CreatedAt { get; set; }
        public bool IsPublic { get; set; }
        public int CategoryId { get; set; }
        public string CategoryTitle { get; set; }
    }

}
