namespace InventoryManagementSystem.BLL.Models
{
    public class SupportTicketModel
    {
        public string ReportedBy { get; set; }
        public int InventoryId { get; set; }
        public string Link { get; set; }
        public string Summary { get; set; }
        public string Priority { get; set; } // High, Average, Low        
        public DateTime CreatedAt { get; set; }
    }
}