using Newtonsoft.Json;
using System.Text.Json.Serialization;

namespace InventoryManagementSystem.Models
{
    public class ValueViewModel
    {
        [JsonPropertyName("id")]
        public int Id { get; set; }

        [JsonPropertyName("inventoryId")]
        public int InventoryId { get; set; }

        [JsonPropertyName("fieldId")]
        public int FieldId { get; set; }

        [JsonPropertyName("typeId")]
        public int TypeId { get; set; }

        [JsonPropertyName("value")]
        public object Value { get; set; }

        [JsonPropertyName("rowNum")]
        public int RowNum { get; set; }
    }
}
