using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using OrdersAPI.DTO;
using OrdersAPI.Models;
using OrdersAPI.ResultsModel;
using OrdersAPI.Services.Interfaces;

namespace OrdersAPI.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class OrderController : ControllerBase
    {
        private readonly IOrderService _orderService;
        private readonly IOrderItemService _orderItemService;

        public OrderController(IOrderService orderService, IOrderItemService orderItemService)
        {
            _orderService = orderService;
            _orderItemService = orderItemService;
        }

        // [HttpGet("filters")]
        // public async Task<ActionResult<FiltersResult>> GetFilters()
        // {
        //     
        // }

        [HttpGet]
        public async Task<ActionResult<List<OrderResult>>> GetOrders(DateTime from, DateTime to)
        {
            var orders = await _orderService.GetOrdersAsync(from, to);
            return Ok(orders);
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<OrderResult>> GetOrder(int id)
        {
            var order = await _orderService.GetOrderAsync(id);
            return Ok(order);
        }

        [HttpPost]
        public async Task<ActionResult<OrderResult>> CreateOrder(OrderModel model)
        {
            var isUnique = await _orderService.IsOrderUnique(model.Number, model.ProviderId);
            if (!isUnique)
                return BadRequest(new {error = "Заказ с таким номером и поставщиком уже существует"});
            var orderDto = new OrderDto()
            {
                Number = model.Number,
                Date = model.Date,
                ProviderId = model.ProviderId
            };
            var order = await _orderService.AddOrderAsync(orderDto);
            if (model.OrderItems != null)
            {
                foreach (var itemModel in model.OrderItems)
                {
                    if (itemModel.Name == order.Number)
                        return BadRequest(new {error = "OrderItem.Name не может быть равен Order.Number"});
                    var itemDto = new OrderItemDto()
                    {
                        Name = itemModel.Name,
                        OrderId = order.Id,
                        Quantity = itemModel.Quantity,
                        Unit = itemModel.Unit
                    };
                    var itemOrder = await _orderItemService.AddOrderItemAsync(itemDto);
                }
            }
            return Ok(order);
        }
        
        [HttpPut("{id}")]
        public async Task<ActionResult<OrderResult>> UpdateOrder(int id, OrderModel model)
        {
            var updateDto = new OrderUpdateDto()
            {
                Id = id,
                Date = model.Date,
                Number = model.Number,
                ProviderId = model.ProviderId
            };
            var updatedOrder = await _orderService.UpdateOrderAsync(updateDto);
            if (model.OrderItems != null)
            {
                foreach (var itemModel in model.OrderItems)
                {
                    if (itemModel.Name == updatedOrder.Number)
                        return BadRequest(new {error = "OrderItem.Name не может быть равен Order.Number"});
                    var itemDto = new OrderItemUpdateDto()
                    {
                        Id = itemModel.Id ?? -1,
                        Name = itemModel.Name,
                        OrderId = updatedOrder.Id,
                        Quantity = itemModel.Quantity,
                        Unit = itemModel.Unit
                    };
                    var itemOrder = await _orderItemService.UpdateOrderItemAsync(itemDto);
                }
            }
            return Ok(updatedOrder);
        }

        [HttpDelete("{id}")]
        public async Task<ActionResult<int>> DeleteOrder(int id)
        {
            var orderId = await _orderService.DeleteOrderAsync(id);
            return Ok(orderId);
        }

        [HttpPost("item")]
        public async Task<ActionResult<OrderItemResult>> AddItemToOrder(OrderItemModel model)
        {
            var itemDto = new OrderItemDto()
            {
                OrderId = model.OrderId ?? -1,
                Name = model.Name,
                Quantity = model.Quantity,
                Unit = model.Unit
            };
            var item = await _orderItemService.AddOrderItemAsync(itemDto);
            return Ok(item);
        }

        [HttpDelete("item/{id}")]
        public async Task<ActionResult<int>> DeleteItem(int id)
        {
            var itemId = await _orderItemService.DeleteOrderItemAsync(id);
            return Ok(itemId);
        }
    }
}