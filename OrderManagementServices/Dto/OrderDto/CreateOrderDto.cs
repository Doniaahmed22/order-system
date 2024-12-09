using OrderManagementData.Entities;
using OrderManagementData.Entities.Enum;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OrderManagementServices.Dto.OrderDto
{
    public class CreateOrderDto
    {

        public int CustomerId { get; set; }
        public DateTime OrderDate { get; set; }
        //public decimal TotalAmount { get; set; }
        public ICollection<OrderItemDto> OrderItems { get; set; } = new List<OrderItemDto>();
        public PaymentMethods PaymentMethod { get; set; }
        //public string Status { get; set; }
    }
}
