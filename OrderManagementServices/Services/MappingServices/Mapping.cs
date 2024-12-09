using AutoMapper;
using OrderManagementData.Entities;
using OrderManagementServices.Dto.CustomerDto;
using OrderManagementServices.Dto.InvoiceDto;
using OrderManagementServices.Dto.OrderDto;
using OrderManagementServices.Dto.Product;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Linq;

namespace OrderManagementServices.Services.MappingServices
{
    public class Mapping : Profile
    {
        public Mapping()
        {
            
            CreateMap<Customer, CreateCustomerDto>();
            CreateMap<CreateCustomerDto, Customer>();

            CreateMap<Customer, GetCustomerDto>();
            CreateMap<GetCustomerDto, Customer>();

            CreateMap<Customer, CustomerOrderDto>();
            CreateMap<CustomerOrderDto, Customer>();

            CreateMap<Customer, GetCustomerOrderDto>();
            CreateMap<GetCustomerOrderDto, Customer>();

            CreateMap<Order, CreateOrderDto>();
            CreateMap<CreateOrderDto, Order>();

            CreateMap<Order, GetOrderDto>();
            CreateMap<GetOrderDto, Order>();

            CreateMap<OrderItem, CustomerOrderItemDto>();

            CreateMap<Product, GetProductDto>();
            CreateMap<GetProductDto, Product>();

            CreateMap<Product, CreateProductDto>();
            CreateMap<CreateProductDto, Product>();

            CreateMap<Invoice, GetInvoiceDto>();
            CreateMap<GetInvoiceDto, Invoice>();

            // Other mappings
        }
    }
}
