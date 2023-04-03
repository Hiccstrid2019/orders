using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using OrdersAPI.DTO;
using OrdersAPI.ResultsModel;

namespace OrdersAPI.Services.Interfaces
{
    public interface IOrderService
    {
        Task<OrderResult> GetOrderAsync(int orderId);
        Task<List<OrderResult>> GetOrdersAsync(DateTime from, DateTime to);
        Task<OrderResult> AddOrderAsync(OrderDto orderDto);
        Task<OrderResult> UpdateOrderAsync(OrderUpdateDto orderUpdateDto);
        Task<int> DeleteOrderAsync(int orderId);
        Task<bool> IsOrderUnique(string number, int providerId);
    }
}