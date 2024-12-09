using AutoMapper;
using OrderManagementData.Entities;
using OrderManagementRepository.Interfaces;
using OrderManagementServices.Dto.CustomerDto;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Linq;

namespace OrderManagementServices.Services.CustomerServices
{
    public class CustomerServices : ICustomerServices
    {
        private readonly IMapper _mapper;
        private readonly IUnitOfWork _unitOfWork;
        private readonly ICustomerRepository _customerRepository;

        public CustomerServices(IMapper mapper, IUnitOfWork unitOfWork, ICustomerRepository customerRepository)
        {
            _mapper = mapper;
            _unitOfWork = unitOfWork;
            _customerRepository = customerRepository;
        }
        public async Task AddCustomer(CreateCustomerDto customerDto)
        {
            var customer = _mapper.Map<Customer>(customerDto);
            await _customerRepository.AddAsync(customer);
            await _unitOfWork.CompleteAsync();
        }

        public async Task<GetCustomerOrderDto> GetAllOrdersOfCustomers(int customerId)
        {

            var customer = await _customerRepository.GetAllOrdersOfCustomer(customerId);

            if (customer == null)
            {
                return null;
            }

            var customerOrderDto = new GetCustomerOrderDto
            {
                CustomerId = customer.CustomerId,
                Name = customer.Name,
                Email = customer.Email,
                Orders = customer.Orders.Select(order => new CustomerOrderDto
                {
                    OrderId = order.OrderId,
                    OrderDate = order.OrderDate,
                    TotalAmount = order.TotalAmount,
                    PaymentMethod = order.PaymentMethod,
                    Status = order.Status,
                    OrderItems = order.OrderItems.Select(item => new CustomerOrderItemDto
                    {
                        OrderItemId = item.OrderItemId,
                        ProductId = item.ProductId,
                        Quantity = item.Quantity,
                        UnitPrice = item.UnitPrice,
                        Discount = item.Discount
                    }).ToList()
                }).ToList()
            };

            return customerOrderDto;

            
        }
    }
}