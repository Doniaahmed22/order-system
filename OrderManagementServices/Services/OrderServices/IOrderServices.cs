using OrderManagementData.Entities;
using OrderManagementServices.Dto.OrderDto;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OrderManagementServices.Services.OrderServices
{
    public interface IOrderServices 
    {
        //Task AddOrder(CreateOrderDto orderDto);
        //Task<GetOrderDto> AddOrder(CreateOrderDto orderDto);
        Task<Order> CreateOrderAsync(CreateOrderDto orderDto);

        Task<GetOrderDto> GetOrderById(int OrderId);

        Task<IEnumerable<GetOrderDto>> GetAllOrders();

        Task UpdateOrderStatus(int OrderId, string newStatus);
    }
}
