using Moq;
using OrderManagementData.Entities.Enum;
using OrderManagementData.Entities;
using OrderManagementRepository.Interfaces;
using OrderManagementServices.Dto.OrderDto;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using OrderManagementServices.Services.OrderServices;
using AutoMapper;
using Microsoft.EntityFrameworkCore;
using OrderManagementData.Context;
using OrderManagementServices.Services.EmailServices;
using Xunit;

namespace TestOrderManagementSystemProject
{
    public class OrderServiceTests
    {
        private readonly Mock<IMapper> _mockMapper;
        private readonly Mock<IUnitOfWork> _mockUnitOfWork;
        private readonly Mock<IOrderRepository> _mockOrderRepository;
        private readonly Mock<IInvoiceRepository> _mockInvoiceRepository;
        private readonly Mock<ICustomerRepository> _mockCustomerRepository;
        private readonly Mock<IEmailServices> _mockEmailServices;
        private readonly OrderManagementDbContext _context;
        private readonly OrderServices _orderService;

        public OrderServiceTests()
        {
            _mockMapper = new Mock<IMapper>();
            _mockUnitOfWork = new Mock<IUnitOfWork>();
            _mockOrderRepository = new Mock<IOrderRepository>();
            _mockInvoiceRepository = new Mock<IInvoiceRepository>();
            _mockCustomerRepository = new Mock<ICustomerRepository>();
            _mockEmailServices = new Mock<IEmailServices>();

            var options = new DbContextOptionsBuilder<OrderManagementDbContext>()
                .UseInMemoryDatabase(databaseName: "TestDatabase")
                .Options;

            _context = new OrderManagementDbContext(options);

            _orderService = new OrderServices(
                _mockMapper.Object,
                _mockUnitOfWork.Object,
                _context,
                _mockOrderRepository.Object,
                _mockInvoiceRepository.Object,
                _mockCustomerRepository.Object,
                _mockEmailServices.Object
            );
        }

        [Fact]
        public async Task CreateOrderAsync_ValidOrder_ShouldReturnOrder()
        {
            // Arrange
            var orderDto = new CreateOrderDto
            {
                CustomerId = 1,
                OrderItems = new List<OrderItemDto>
                {
                    new OrderItemDto { ProductId = 1, Quantity = 2 }
                },
                PaymentMethod = PaymentMethods.CreditCard
            };

            var product = new Product { ProductId = 1, Name = "Egg", Stock = 100, Price = 50m };

            _context.Products.Add(product);
            await _context.SaveChangesAsync();

            _mockUnitOfWork.Setup(uow => uow.CompleteAsync()).ReturnsAsync(1);

            // Act
            var result = await _orderService.CreateOrderAsync(orderDto);

            // Assert
            Assert.NotNull(result);
            Assert.Equal(1, result.CustomerId);
            Assert.Single(result.OrderItems);

            // Verify that CompleteAsync was called twice
            _mockUnitOfWork.Verify(uow => uow.CompleteAsync(), Times.Exactly(2));
        }

        [Fact]
        public async Task CreateOrderAsync_InvalidStock_ShouldThrowException()
        {
            // Arrange
            var orderDto = new CreateOrderDto
            {
                CustomerId = 1,
                OrderItems = new List<OrderItemDto>
                {
                    new OrderItemDto { ProductId = 2, Quantity = 15 } // Quantity exceeds stock
                },
                PaymentMethod = PaymentMethods.CreditCard
            };

            var product = new Product { ProductId = 2, Name = "Milk", Stock = 10, Price = 50m };

            _context.Products.Add(product);
            await _context.SaveChangesAsync();

            // Act & Assert
            await Assert.ThrowsAsync<InvalidOperationException>(() => _orderService.CreateOrderAsync(orderDto));
        }

        [Fact]
        public async Task UpdateStock_ValidOrder_ShouldUpdateStock()
        {
            // Arrange
            var orderDto = new CreateOrderDto
            {
                CustomerId = 1,
                OrderItems = new List<OrderItemDto>
                {
                    new OrderItemDto { ProductId = 3, Quantity = 2 }
                },
                PaymentMethod = PaymentMethods.CreditCard
            };

            var product = new Product { ProductId = 3, Name = "Butter", Stock = 20, Price = 50m };

            _context.Products.Add(product);
            await _context.SaveChangesAsync();

            _mockUnitOfWork.Setup(uow => uow.CompleteAsync()).ReturnsAsync(1);

            // Act
            var result = await _orderService.CreateOrderAsync(orderDto);

            // Assert
            Assert.NotNull(result);
            var updatedProduct = await _context.Products.FindAsync(3);
            Assert.Equal(18, updatedProduct.Stock); // Stock should be reduced by 2
        }

        [Fact]
        public async Task CreateOrderAsync_ValidOrder_ShouldHandlePaymentCorrectly()
        {
            // Arrange
            var orderDto = new CreateOrderDto
            {
                CustomerId = 1,
                OrderItems = new List<OrderItemDto>
                {
                    new OrderItemDto { ProductId = 4, Quantity = 2 }
                },
                PaymentMethod = PaymentMethods.PayPal
            };

            var product = new Product { ProductId = 4, Name = "Juice", Stock = 10, Price = 50m };

            _context.Products.Add(product);
            await _context.SaveChangesAsync();

            _mockUnitOfWork.Setup(uow => uow.CompleteAsync()).ReturnsAsync(1);

            // Act
            var result = await _orderService.CreateOrderAsync(orderDto);

            // Assert
            Assert.NotNull(result);
            Assert.Equal(PaymentMethods.PayPal, result.PaymentMethod);
        }
    }
}








/*
using Moq;
using OrderManagementData.Entities.Enum;
using OrderManagementData.Entities;
using OrderManagementRepository.Interfaces;
using OrderManagementServices.Dto.OrderDto;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using OrderManagementServices.Services.OrderServices;
using AutoMapper;
using Microsoft.EntityFrameworkCore;
using OrderManagementData.Context;
using OrderManagementServices.Services.EmailServices;

namespace TestOrderManagementSystemProject
{
    public class OrderServiceTests
    {
        private readonly Mock<IMapper> _mockMapper;
        private readonly Mock<IUnitOfWork> _mockUnitOfWork;
        private readonly Mock<IOrderRepository> _mockOrderRepository;
        private readonly Mock<IInvoiceRepository> _mockInvoiceRepository;
        private readonly Mock<ICustomerRepository> _mockCustomerRepository;
        private readonly Mock<IEmailServices> _mockEmailServices;
        private readonly OrderManagementDbContext _context;
        private readonly OrderServices _orderService;

        public OrderServiceTests()
        {
            _mockMapper = new Mock<IMapper>();
            _mockUnitOfWork = new Mock<IUnitOfWork>();
            _mockOrderRepository = new Mock<IOrderRepository>();
            _mockInvoiceRepository = new Mock<IInvoiceRepository>();
            _mockCustomerRepository = new Mock<ICustomerRepository>();
            _mockEmailServices = new Mock<IEmailServices>();

            var options = new DbContextOptionsBuilder<OrderManagementDbContext>()
                .UseInMemoryDatabase(databaseName: "TestDatabase")
                .Options;

            _context = new OrderManagementDbContext(options);

            _orderService = new OrderServices(
                _mockMapper.Object,
                _mockUnitOfWork.Object,
                _context,
                _mockOrderRepository.Object,
                _mockInvoiceRepository.Object,
                _mockCustomerRepository.Object,
                _mockEmailServices.Object
            );
        }

        [Fact]

        public async Task CreateOrderAsync_ValidOrder_ShouldReturnOrder()
        {
            // Arrange
            var orderDto = new CreateOrderDto
            {
                CustomerId = 1,
                OrderItems = new List<OrderItemDto>
                {
                    new OrderItemDto { ProductId = 1, Quantity = 2 }
                },
                PaymentMethod = PaymentMethods.CreditCard
            };

            var product = new Product { ProductId = 1, Name = "Egg", Stock = 10, Price = 50m };

            _context.Products.Add(product);
            await _context.SaveChangesAsync();

            _mockUnitOfWork.Setup(uow => uow.CompleteAsync()).ReturnsAsync(1);

            // Act
            var result = await _orderService.CreateOrderAsync(orderDto);

            // Assert
            Assert.NotNull(result);
            Assert.Equal(1, result.CustomerId);
            Assert.Single(result.OrderItems);

            // تحقق من أن CompleteAsync تم استدعاؤها مرتين إذا كان هذا هو التوقع الصحيح
            _mockUnitOfWork.Verify(uow => uow.CompleteAsync(), Times.Exactly(2));
        }
       

        [Fact]
        public async Task CreateOrderAsync_InvalidStock_ShouldThrowException()
        {
            // Arrange
            var orderDto = new CreateOrderDto
            {
                CustomerId = 1,
                OrderItems = new List<OrderItemDto>
                {
                    new OrderItemDto { ProductId = 1, Quantity = 15 } // Quantity exceeds stock
                },
                PaymentMethod = PaymentMethods.CreditCard
            };

            var product = new Product { ProductId = 2, Name="Milk",  Stock = 10, Price = 50m };

            _context.Products.Add(product);
            await _context.SaveChangesAsync();

            // Act & Assert
            await Assert.ThrowsAsync<InvalidOperationException>(() => _orderService.CreateOrderAsync(orderDto));
        }
    }
}
*/

