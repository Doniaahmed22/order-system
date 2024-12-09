using OrderManagementServices.Dto.OrderDto;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OrderManagementServices.Dto.CustomerDto
{
    public class GetCustomerOrderDto
    {
        public int CustomerId { get; set; }
        public string Name { get; set; }
        public string Email { get; set; }
        public List<CustomerOrderDto> Orders { get; set; }
    }
}
