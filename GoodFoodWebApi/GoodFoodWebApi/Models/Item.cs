using System.ComponentModel.DataAnnotations;
using System.Text.Json.Serialization;

namespace WebApplication1.Models
{
    public class Item
    {
        [Key]
        [JsonPropertyName("itemId")]
        public int ItemId { get; set; }
        [Required]
        [JsonPropertyName("itemLabel")]
        public string ItemLabel { get; set; }
        [Required]
        [JsonPropertyName("itemPrice")]
        public double ItemPrice { get; set; }
        [Required]
        [JsonPropertyName("itemCategoryId")]
        public int ItemCategoryId { get; set; }
        [JsonPropertyName("itemDescription")]
        public string ItemDescription { get; set; }
        [JsonPropertyName("itemPictureUrl")]
        public string ItemPictureUrl { get; set; }
    }
}