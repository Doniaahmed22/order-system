using AutoMapper;
using Microsoft.EntityFrameworkCore;
using OrderManagementData.Context;
using OrderManagementData.Entities;
using OrderManagementData.Entities.Enum;
using OrderManagementRepository.Interfaces;
using OrderManagementRepository.Repositories;
using OrderManagementServices.Dto.CustomerDto;
using OrderManagementServices.Dto.OrderDto;
using OrderManagementServices.Dto.UserDto;
using OrderManagementServices.Services.EmailServices;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;

namespace OrderManagementServices.Services.OrderServices
{
    public class OrderServices : IOrderServices
    {
        private readonly IMapper _mapper;
        private readonly IUnitOfWork _unitOfWork;
        private readonly OrderManagementDbContext _context;
        private readonly IOrderRepository _orderRepository;
        private readonly IInvoiceRepository _invoiceRepository;
        private readonly ICustomerRepository _customerRepository;
        private readonly IEmailServices _emailServices;

        public OrderServices(IMapper mapper, IUnitOfWork unitOfWork, OrderManagementDbContext context, IOrderRepository orderRepository, IInvoiceRepository invoiceRepository, ICustomerRepository customerRepository, IEmailServices emailServices) 
        {
            _mapper = mapper;
            _unitOfWork = unitOfWork;
            _context = context;
            _orderRepository = orderRepository;
            _invoiceRepository = invoiceRepository;
            _customerRepository = customerRepository;
            _emailServices = emailServices;
        }



        public async Task<Order> CreateOrderAsync(CreateOrderDto orderDto)
        {

            var paymentMethod = (PaymentMethods)orderDto.PaymentMethod;
            ValidatePaymentMethod(paymentMethod);
            var order = new Order
            {
                CustomerId = orderDto.CustomerId,
                OrderDate = orderDto.OrderDate,
                TotalAmount = 0,
                PaymentMethod = orderDto.PaymentMethod,
                OrderItems = new List<OrderItem>()
            };

            foreach (var itemDto in orderDto.OrderItems)
            {
                var product = await _context.Products.FindAsync(itemDto.ProductId);
                if (product == null || product.Stock < itemDto.Quantity)
                {
                    throw new InvalidOperationException("Insufficient stock for product " + itemDto.ProductId);
                }

                product.Stock -= itemDto.Quantity;

                var orderItem = new OrderItem
                {
                    ProductId = itemDto.ProductId,
                    Quantity = itemDto.Quantity,
                    UnitPrice = product.Price,
                    Discount = itemDto.Discount
                };

                order.OrderItems.Add(orderItem);
                order.TotalAmount += (orderItem.UnitPrice - (orderItem.UnitPrice * (orderItem.Discount/100))) * orderItem.Quantity ;
            }

            ApplyDiscounts(order);

            ProcessPayment(order);

            _orderRepository.AddAsync(order);
            await _unitOfWork.CompleteAsync();

            var invoice = GenerateInvoice(order);
            _invoiceRepository.AddAsync(invoice);
            await _unitOfWork.CompleteAsync();
            return order;
        }


        private void ValidatePaymentMethod(PaymentMethods paymentMethod)
        {
            if (!Enum.IsDefined(typeof(PaymentMethods), paymentMethod))
            {
                throw new InvalidOperationException("Unsupported payment method");
            }
        }


        private void ApplyDiscounts(Order order)
        {
            if (order.TotalAmount > 200)
            {
                order.TotalAmount *= 0.9m; // 10% discount
            }
            else if (order.TotalAmount > 100)
            {
                order.TotalAmount *= 0.95m; // 5% discount
            }
        }


        private void ProcessPayment(Order order)
        {
            switch (order.PaymentMethod)
            {
                case PaymentMethods.CreditCard:
                    order.Status = "Paid"; 
                    break;
                case PaymentMethods.PayPal:
                    order.Status = "Paid"; 
                    break;
                case PaymentMethods.BankTransfer:
                    order.Status = "Pending"; 
                    break;
                case PaymentMethods.CashOnDelivery:
                    order.Status = "Pending"; 
                    break;
                default:
                    throw new InvalidOperationException("Unsupported payment method");
            }
        }


        private Invoice GenerateInvoice(Order order)
        {
            return new Invoice
            {
                OrderId = order.OrderId,
                InvoiceDate = DateTime.UtcNow,
                TotalAmount = order.TotalAmount
            };
        }

        public async Task<GetOrderDto> GetOrderById(int OrderId)
        {
           
            var GetOrder = await _orderRepository.GetByIdAsync(OrderId);
            return _mapper.Map<GetOrderDto>(GetOrder);
        }

        public async Task<IEnumerable<GetOrderDto>> GetAllOrders()
        {
            var orders = await _orderRepository.GetAllAsync();
            return _mapper.Map<IEnumerable<GetOrderDto>>(orders);
            
        }

        public async Task UpdateOrderStatus(int OrderId, string newStatus)
        {

            var order = await _orderRepository.GetByIdAsync(OrderId);
            if (order == null)
            {
                throw new Exception("Order not found");
            }
            string oldStatus = order.Status;
            order.Status = newStatus;
            _orderRepository.Update(order);
            await _unitOfWork.CompleteAsync();
            await SendEmail(order.CustomerId, oldStatus, newStatus);


        }

        public async Task<string> SendEmail(int customerId, string oldStatus, string newStatus)
        {
            Customer customer = await _customerRepository.GetByIdAsync(customerId);
            var receiver = customer.Email;
            // Prepare email
            string subject = "Welcome to Our Service";
            string body = $"Hello {customer.Name},<br/>Your order status changed from {oldStatus} to {newStatus}";

            // Send email
            await _emailServices.SendEmailAsync(receiver, subject, body);

            return "done";
        }

    }
}
