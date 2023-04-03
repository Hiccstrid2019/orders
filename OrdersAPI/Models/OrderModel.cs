using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace OrdersAPI.Models
{
    public class OrderModel
    {
        [Required]
        public string Number { get; set; }
        [Required]
        public DateTime Date { get; set; }
        [Required]
        public int ProviderId { get; set; }
        public List<OrderItemModel>? OrderItems { get; set; }
    }
}