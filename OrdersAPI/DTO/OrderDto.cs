using System;

namespace OrdersAPI.DTO
{
    public class OrderDto
    {
        public string Number { get; set; }
        public DateTime Date { get; set; }
        public int ProviderId { get; set; }
    }
}