using System.ComponentModel.DataAnnotations;

namespace OrdersAPI.Models
{
    public class OrderItemModel
    {
        public int? Id { get; set; }
        public int? OrderId { get; set; }
        [Required]
        public string Name { get; set; }
        [Range(double.Epsilon, double.MaxValue)]
        public decimal Quantity { get; set; }
        [Required]
        public string Unit { get; set; }
    }
}