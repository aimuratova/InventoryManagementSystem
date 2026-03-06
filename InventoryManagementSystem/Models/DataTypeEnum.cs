namespace InventoryManagementSystem.Models
{
    public enum DataTypeEnum
    {
        CreatedBy = 1, // string not shown
        CreatedAt = 2, // datetime not shown
        CustomId = 3, // string not shown

        Singleline1 = 4, // string
        Singleline2 = 5, // string
        Singleline3 = 6, // string

        Multiline1 = 7, // string multi
        Multiline2, // string multi
        Multiline3, // string multi

        Num1 = 10, // int
        Num2,
        Num3,

        ImageUrl1 = 13, // string img
        ImageUrl2,
        ImageUrl3,

        Check1 = 16, // bool
        Check2,
        Check3,

        Datetime1 = 19, // datetime
        Datetime2,
        Datetime3,
    }
}
