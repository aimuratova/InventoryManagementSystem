namespace InventoryManagementSystem.Models
{
    public class UserInfoViewModel : UserViewModel
    {
        public bool IsAdmin { get; set; }
        public Dictionary<int, string>? Inventories { get; set; }
        public Dictionary<int, string> SelectedInventories { get; set; }
    }
}
