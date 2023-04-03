using System;
using System.ComponentModel.DataAnnotations.Schema;

namespace OrdersAPI.Data
{
    [Table("Order")]
    public class Order
    {
        public int Id { get; set; }
        public string Number { get; set; }
        public DateTime Date { get; set; }
        public Provider Provider { get; set; }
        public int ProviderId { get; set; }
    }
}