using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using OrdersAPI.Data;
using OrdersAPI.DTO;
using OrdersAPI.ResultsModel;
using OrdersAPI.Services.Interfaces;

namespace OrdersAPI.Services
{
    public class OrderItemService : IOrderItemService
    {
        private readonly AppDbContext _context;

        public OrderItemService(AppDbContext context)
        {
            _context = context;
        }

        public async Task<OrderItemResult> AddOrderItemAsync(OrderItemDto itemDto)
        {
            var newItem = new OrderItem()
            {
                OrderId = itemDto.OrderId,
                Name = itemDto.Name,
                Quantity = itemDto.Quantity,
                Unit = itemDto.Unit
            };
            await _context.OrderItems.AddAsync(newItem);
            await _context.SaveChangesAsync();
            return new OrderItemResult()
            {
                Id = newItem.Id,
                Name = newItem.Name,
                OrderId = newItem.OrderId,
                Quantity = newItem.Quantity,
                Unit = newItem.Unit
            };
        }

        public async Task<OrderItemResult> UpdateOrderItemAsync(OrderItemUpdateDto updateDto)
        {
            var item = await _context.OrderItems.FirstOrDefaultAsync(o => o.Id == updateDto.Id);
            item.Name = updateDto.Name;
            item.Quantity = updateDto.Quantity;
            item.Unit = updateDto.Unit;
            await _context.SaveChangesAsync();
            return new OrderItemResult()
            {
                Id = item.Id,
                Name = item.Name,
                OrderId = item.OrderId,
                Quantity = item.Quantity,
                Unit = item.Unit
            };
        }

        public async Task<int> DeleteOrderItemAsync(int itemId)
        {
            var delItem = new OrderItem() {Id = itemId};
            _context.OrderItems.Attach(delItem);
            _context.OrderItems.Remove(delItem);
            await _context.SaveChangesAsync();
            return itemId;
        }
    }
}