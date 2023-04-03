using System.Threading.Tasks;
using OrdersAPI.DTO;
using OrdersAPI.ResultsModel;

namespace OrdersAPI.Services.Interfaces
{
    public interface IOrderItemService
    {
        Task<OrderItemResult> AddOrderItemAsync(OrderItemDto itemDto);
        Task<OrderItemResult> UpdateOrderItemAsync(OrderItemUpdateDto updateDto);
        Task<int> DeleteOrderItemAsync(int itemId);
    }
}