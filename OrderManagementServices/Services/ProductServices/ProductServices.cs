using AutoMapper;
using OrderManagementData.Entities;
using OrderManagementRepository.Interfaces;
using OrderManagementServices.Dto.OrderDto;
using OrderManagementServices.Dto.Product;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OrderManagementServices.Services.ProductServices
{
    public class ProductServices : IProductServices
    {
        private readonly IMapper _mapper;
        private readonly IUnitOfWork _unitOfWork;
        private readonly IProductRepository _productRepository;

        public ProductServices(IMapper mapper, IUnitOfWork unitOfWork, IProductRepository productRepository)
        {
            _mapper = mapper;
            _unitOfWork = unitOfWork;
            _productRepository = productRepository;
        }

        public async Task AddProduct(CreateProductDto productDto)
        {
            if(productDto.Price <= 0)
            {
                throw new Exception("Invalid price");
            }
            if (productDto.Stock < 0)
            {
                throw new Exception("Invalid stock amount");
            }
            var product = _mapper.Map<Product>(productDto);
            await _productRepository.AddAsync(product);
            await _unitOfWork.CompleteAsync();
        }

        public async Task<GetProductDto> GetProductById(int ProductId)
        {
            var GetProduct = await _productRepository.GetByIdAsync(ProductId);
            return _mapper.Map<GetProductDto>(GetProduct);
        }

        public async Task<IEnumerable<GetProductDto>> GetAllProducts()
        {
            var products = await _productRepository.GetAllAsync();
            return _mapper.Map<IEnumerable<GetProductDto>>(products);
        }

        public async Task UpdateProduct(int ProductId, CreateProductDto productDto)
        {

            var product = await _productRepository.GetByIdAsync(ProductId);
            if (product == null)
            {
                throw new Exception("Product not found");
            }

            if (productDto.Price <= 0)
            {
                throw new Exception("Invalid price");
            }
            if (productDto.Stock < 0)
            {
                throw new Exception("Invalid stock amount");
            }

            product.Price = productDto.Price;
            product.Stock = productDto.Stock;
            product.Name = productDto.Name;
            _productRepository.Update(product);
            await _unitOfWork.CompleteAsync();
        }
    }
}
