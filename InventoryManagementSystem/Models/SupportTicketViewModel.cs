namespace InventoryManagementSystem.Models
{
    public class SupportTicketViewModel
    {
        public string ReportedBy { get; set; }
        public int InventoryId { get; set; }
        public string Link { get; set; }
        public string Summary { get; set; }
        public string Priority { get; set; } // High, Average, Low        
        public DateTime CreatedAt { get; set; }
    }
}
