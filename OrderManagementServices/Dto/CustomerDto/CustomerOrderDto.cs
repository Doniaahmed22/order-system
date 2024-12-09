using OrderManagementData.Entities;
using OrderManagementData.Entities.Enum;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OrderManagementServices.Dto.CustomerDto
{
    public class CustomerOrderDto
    {
        public int OrderId { get; set; }
        public DateTime OrderDate { get; set; }
        public decimal TotalAmount { get; set; }
        public PaymentMethods PaymentMethod { get; set; }
        public string Status { get; set; }
        public List<CustomerOrderItemDto> OrderItems { get; set; }
    }
}
