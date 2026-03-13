namespace InventoryManagementSystem.Models
{
    public class InventoryValueViewModel
    {
        public InventoryItemViewModel BasicInfo { get; set; }
        public List<FieldVM>? Fields { get; set; }
        public RowValueViewModel MainInfo { get; set; }
    }

    public class RowValueViewModel
    {
        public int Id { get; set; }
        public int RowNum { get; set; }
        public string CreatedBy { get; set; } = string.Empty;
        public DateTime CreatedAt { get; set; }
        public string? CustomId { get; set; }
        public string? Singleline1 { get; set; }
        public string? Singleline2 { get; set; }
        public string? Singleline3 { get; set; }
        public string? Multiline1 { get; set; }
        public string? Multiline2 { get; set; }
        public string? Multiline3 { get; set; }
        public int? Num1 { get; set; }
        public int? Num2 { get; set; }
        public int? Num3 { get; set; }
        public string? ImageUrl1 { get; set; }
        public string? ImageUrl2 { get; set; }
        public string? ImageUrl3 { get; set; }
        public bool? Check1 { get; set; }
        public bool? Check2 { get; set; }
        public bool? Check3 { get; set; }
        public DateTime? Datetime1 { get; set; }
        public DateTime? Datetime2 { get; set; }
        public DateTime? Datetime3 { get; set; }
    }
}
