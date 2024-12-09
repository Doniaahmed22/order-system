using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using OrderManagementServices.Dto.CustomerDto;
using OrderManagementServices.Dto.OrderDto;
using OrderManagementServices.Services.CustomerServices;
using OrderManagementServices.Services.OrderServices;

namespace OrderManagementApi.Controllers
{
    [ApiController]
    public class OrderController : ControllerBase
    {
        private readonly IOrderServices _orderServices;

        public OrderController(IOrderServices orderServices)
        {
            _orderServices = orderServices;
        }
        [HttpPost]
        [Route("api/Orders")]
        public async Task<ActionResult> CreateNewOrder(CreateOrderDto orderDto)
        {
            if (orderDto == null)
            {
                return BadRequest("invalid inputs");
            }

            await _orderServices.CreateOrderAsync(orderDto);
            return Ok(orderDto);
        }

        [HttpGet]
        [Route("api/orders/{OrderId}")]
        public async Task<ActionResult> GetDetailsOfSpecificOrder(int OrderId)
        {
            if (OrderId == 0)
            {
                return BadRequest("OrderId mustn't be zero");
            }

            var Order = await _orderServices.GetOrderById(OrderId);
            return Ok(Order);
        }

        [HttpGet]
        [Route("api/orders/GetAllOrders")]
        [Authorize(Roles = "Admin")]
        public async Task<ActionResult<IEnumerable<GetOrderDto>>> GetAllOrders()
        {
            var Orders = await _orderServices.GetAllOrders();
            return Ok(Orders);
        }

        [HttpPut]
        //[Route("api/orders/{orderId}/status")]
        [Route("api/orders/{OrderId}/status")]
        [Authorize(Roles ="Admin")]
        public async Task<ActionResult> UpdateStatusOfOrder(int OrderId, string status)
        {
            if (OrderId == 0)
            {
                return BadRequest("OrderId mustn't be zero");
            }

            await _orderServices.UpdateOrderStatus(OrderId, status);
            //await _orderServices.SendEmail(OrderId, status);

            return Ok();
        }

    }
}
