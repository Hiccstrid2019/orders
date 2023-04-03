using System.ComponentModel.DataAnnotations.Schema;

namespace OrdersAPI.Data
{
    [Table("OrderItem")]
    public class OrderItem
    {
        public int Id { get; set; }
        public Order Order { get; set; }
        public int OrderId { get; set; }
        public string Name { get; set; }
        public decimal Quantity { get; set; }
        public string Unit { get; set; }
    }
}