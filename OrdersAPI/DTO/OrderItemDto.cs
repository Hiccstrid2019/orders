namespace OrdersAPI.DTO
{
    public class OrderItemDto
    {
        public int OrderId { get; set; }
        public string Name { get; set; }
        public decimal Quantity { get; set; }
        public string Unit { get; set; }
    }
}