namespace InventoryManagementSystem.Models
{
    public class InventoryEditViewModel : InventoryItemDetailVM
    {
        public Dictionary<int, string> Categories { get; set; }
        public List<UserViewModel> RegisteredUsers { get; set; }
        public List<string> SelectedUserIds { get; set; }
    }
}
