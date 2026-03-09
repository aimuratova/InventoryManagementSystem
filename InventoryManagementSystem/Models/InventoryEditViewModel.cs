namespace InventoryManagementSystem.Models
{
    public class InventoryEditViewModel : InventoryItemDetailVM
    {
        public Dictionary<int, string> Categories { get; set; }
        public List<UserViewModel> RegisteredUsers { get; set; }
        public List<string> SelectedUserIds { get; set; }
        public List<FieldTypeViewModel> FieldTypeOptions { get; set; }
        public List<CustomIdTypeViewModel> CustomIdTypeOptions { get; set; }
        public List<CustomIdTypeViewModel> SelectedCustomTypes { get; set; }
    }

    public class FieldTypeViewModel
    {
        public int Id { get; set; }
        public string Title { get; set; }
        public string DataType { get; set; }
    }

    public class CustomIdTypeViewModel
    {
        public int Id { get; set; }
        public string Title { get; set; }
        public string TooltipText { get; set; }
        public string PatternValue { get; set; }
        public int OrderNum { get; set; }
    }
}
