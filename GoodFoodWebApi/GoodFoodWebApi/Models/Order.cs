using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text.Json.Serialization;

namespace WebApplication1.Models
{
    public class Order
    {
        [Key]
        [JsonPropertyName("orderId")]
        public int OrderId { get; set; }
        [Required]
        [JsonPropertyName("orderType")]
        public string OrderType { get; set; }
        [JsonPropertyName("address")]
        public string Address { get; set; }
        [Required]
        [JsonPropertyName("customerEmail")]
        public string CustomerEmail { get; set; }
        [Required]
        [JsonPropertyName("customerName")]
        public string CustomerName { get; set; }
        [Required]
        [JsonPropertyName("customerPhoneNumber")]
        public string CustomerPhoneNumber { get; set; }
        [Required]
        [JsonPropertyName("orderTotalPrice")]
        public double OrderTotalPrice { get; set; }
        [Required]
        [JsonPropertyName("orderItems")]
        public List<OrderItem> OrderItems { get; set; }
    }
}