using OrderManagementServices.Dto.OrderDto;
using OrderManagementServices.Dto.Product;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OrderManagementServices.Services.ProductServices
{
    public interface IProductServices
    {
        Task AddProduct(CreateProductDto productDto);

        Task<GetProductDto> GetProductById(int ProductId);

        Task<IEnumerable<GetProductDto>> GetAllProducts();

        Task UpdateProduct(int ProductId, CreateProductDto productDto);
    }
}
