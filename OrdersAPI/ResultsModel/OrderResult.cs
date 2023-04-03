using System;
using System.Collections.Generic;

namespace OrdersAPI.ResultsModel
{
    public class OrderResult
    {
        public int Id { get; set; }
        public string Number { get; set; }
        public DateTime Date { get; set; }
        public int ProviderId { get; set; }
        public string ProviderName { get; set; }
        public List<OrderItemResult> OrderItems { get; set; }
    }
}