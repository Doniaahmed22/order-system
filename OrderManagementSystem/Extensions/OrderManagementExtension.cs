using OrderManagementRepository.Interfaces;
using OrderManagementRepository.Repositories;
using OrderManagementServices.Services.CustomerServices;
using OrderManagementServices.Services.EmailServices;
using OrderManagementServices.Services.InvoiceServices;
using OrderManagementServices.Services.MappingServices;
using OrderManagementServices.Services.OrderServices;
using OrderManagementServices.Services.ProductServices;
using OrderManagementServices.Services.TokenServices;
using OrderManagementServices.Services.UserServices;

namespace OrderManagementApi.Extensions
{
    public static class OrderManagementExtension
    {
        public static IServiceCollection AddOrderServices(this IServiceCollection services)
        {
            services.AddScoped<IUnitOfWork, UnitOfWork>();
            services.AddScoped<ICustomerRepository, CustomerRepository>();
            services.AddScoped<ICustomerServices, CustomerServices>();
            services.AddScoped<IProductServices, ProductServices>();
            services.AddScoped<IOrderServices, OrderServices>();
            services.AddScoped<IInvoiceServices, InvoiceServices>();
            services.AddScoped<IUserServices, UserServices>();
            services.AddScoped<ITokenServices, TokenServices>();
            services.AddScoped<IInvoiceRepository, InvoiceRepository>();
            services.AddScoped<IOrderRepository, OrderRepository>();
            services.AddScoped<IEmailServices, EmailServices>();
            services.AddScoped<IProductRepository, ProductRepository>();


            services.AddAutoMapper(typeof(Mapping));


            return services;
        }
    }
}
