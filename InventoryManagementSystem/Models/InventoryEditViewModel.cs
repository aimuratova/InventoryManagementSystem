namespace InventoryManagementSystem.Models
{
    public class InventoryEditViewModel : InventoryItemDetailVM
    {
        public Dictionary<int, string> Categories { get; set; }
        public List<UserViewModel> RegisteredUsers { get; set; }
        public List<string> SelectedUserIds { get; set; }
        public List<FieldTypeViewModel> FieldTypeOptions { get; set; }
    }

    public class FieldTypeViewModel
    {
        public int Id { get; set; }
        public string Title { get; set; }
        public string DataType { get; set; }
    }
}
