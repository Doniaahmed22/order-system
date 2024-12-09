using OrderManagementData.Entities;
using OrderManagementServices.Dto.CustomerDto;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OrderManagementServices.Services.CustomerServices
{
    public interface ICustomerServices
    {
        Task AddCustomer(CreateCustomerDto customerDto);
        Task<GetCustomerOrderDto> GetAllOrdersOfCustomers(int customerId);
    }
}
