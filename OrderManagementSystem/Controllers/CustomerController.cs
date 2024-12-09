using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;
using OrderManagementData.Entities;
using OrderManagementRepository.Dto;
using OrderManagementRepository.Interfaces;
using OrderManagementRepository.Repositories;
using OrderManagementServices.Dto.CustomerDto;
using OrderManagementServices.Services.CustomerServices;
using System.Text;

namespace OrderManagementApi.Controllers
{
    [ApiController]
    public class CustomerController : ControllerBase
    {
        private readonly ICustomerServices _customerServices;

        public CustomerController(ICustomerServices customerServices)
        {
            _customerServices = customerServices;
        }
        [HttpPost]
        [Route("api/customers")]
        public async Task<ActionResult> CreateNewCustomer(CreateCustomerDto customerDto)
        {
            if (customerDto == null)
            {
                return BadRequest("invalid inputs");
            }

            await _customerServices.AddCustomer(customerDto);
            return Ok(customerDto);
        }

        [HttpGet]
        [Route("api/{customerId}/orders")]
        public async Task<ActionResult<GetCustomerOrderDto>> GetAllOrdersForACustomer (int customerId)
        {
            if (customerId == 0)
            {
                return BadRequest("customerId mustn't be zero");
            }

            var customerOrders = await _customerServices.GetAllOrdersOfCustomers(customerId);
            return Ok(customerOrders);
        }


    }
}
