using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using OrdersAPI.Data;
using OrdersAPI.DTO;
using OrdersAPI.ResultsModel;
using OrdersAPI.Services.Interfaces;

namespace OrdersAPI.Services
{
    public class OrderService : IOrderService
    {
        private readonly AppDbContext _context;

        public OrderService(AppDbContext context)
        {
            _context = context;
        }
        public async Task<OrderResult> GetOrderAsync(int orderId)
        {
            var order = await _context.Orders
                .Where(o => o.Id == orderId)
                .Select(order => new OrderResult()
                {
                    Id = order.Id,
                    Number = order.Number,
                    Date = order.Date,
                    ProviderId = order.ProviderId,
                    ProviderName = _context.Providers
                        .FirstOrDefault(p => p.Id == order.ProviderId).Name,
                    OrderItems = _context.OrderItems
                        .Where(item => item.OrderId == orderId)
                        .Select(item => new OrderItemResult()
                        {
                            Id = item.Id,
                            Name = item.Name,
                            Quantity = item.Quantity,
                            Unit = item.Unit,
                            OrderId = item.OrderId
                        }).ToList()
                })
                .FirstOrDefaultAsync();
            return order;
        }

        public async Task<List<OrderResult>> GetOrdersAsync([FromQuery] DateTime from, [FromQuery] DateTime to)
        {
            var orders = await _context.Orders
                .Where(order => order.Date >= from && order.Date <= to)
                .Select(order => new OrderResult()
                {
                    Id = order.Id,
                    Number = order.Number,
                    Date = order.Date,
                    ProviderId = order.ProviderId,
                    ProviderName = _context.Providers
                        .FirstOrDefault(p => p.Id == order.ProviderId).Name,
                    OrderItems = _context.OrderItems
                        .Where(item => item.OrderId == order.Id)
                        .Select(item => new OrderItemResult()
                        {
                            Id = item.Id,
                            Name = item.Name,
                            Quantity = item.Quantity,
                            Unit = item.Unit,
                            OrderId = item.OrderId
                        }).ToList()
                }).ToListAsync();
            return orders;
        }

        public async Task<OrderResult> AddOrderAsync(OrderDto orderDto)
        {
            var order = new Order()
            {
                Number = orderDto.Number,
                Date = orderDto.Date,
                ProviderId = orderDto.ProviderId
            };
            await _context.Orders.AddAsync(order);
            await _context.SaveChangesAsync();
            return new OrderResult()
            {
                Id = order.Id,
                Number = order.Number,
                Date = order.Date,
                ProviderId = order.ProviderId
            };
        }

        public async Task<OrderResult> UpdateOrderAsync(OrderUpdateDto orderUpdateDto)
        {
            var order = await _context.Orders.FirstOrDefaultAsync(o => o.Id == orderUpdateDto.Id);
            order.ProviderId = orderUpdateDto.ProviderId;
            order.Date = orderUpdateDto.Date;
            order.Number = orderUpdateDto.Number;
            await _context.SaveChangesAsync();
            return new OrderResult()
            {
                Id = order.Id,
                Number = order.Number,
                Date = order.Date,
                ProviderId = order.ProviderId
            };
        }

        public async Task<int> DeleteOrderAsync(int orderId)
        {
            var delOrder = new Order() {Id = orderId};
            _context.Orders.Attach(delOrder);
            _context.Orders.Remove(delOrder);
            await _context.SaveChangesAsync();
            return orderId;
        }

        public async Task<bool> IsOrderUnique(string number, int providerId)
        {
            var order = await _context.Orders.FirstOrDefaultAsync(o =>
                o.Number == number && o.ProviderId == providerId);
            return order == null;
        }
    }
}