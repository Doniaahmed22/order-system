using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using OrderManagementServices.Dto.Product;
using OrderManagementServices.Services.ProductServices;

namespace OrderManagementApi.Controllers
{
    [ApiController]
    public class ProductController : ControllerBase
    {
        private readonly IProductServices _productServices;

        public ProductController(IProductServices productServices)
        {
            _productServices = productServices;
        }
        [HttpPost]
        [Route("api/Products")]
        [Authorize(Roles = "Admin")]
        public async Task<ActionResult> CreateNewProduct(CreateProductDto productDto)
        {
            if (productDto == null)
            {
                return BadRequest("invalid inputs");
            }

            await _productServices.AddProduct(productDto);
            return Ok(productDto);
        }

        [HttpGet]
        [Route("api/products/{ProductId}")]
        public async Task<ActionResult> GetDetailsOfSpecificProduct(int ProductId)
        {
            if (ProductId == 0)
            {
                return BadRequest("ProductId mustn't be zero");
            }

            var product = await _productServices.GetProductById(ProductId);
            return Ok(product);
        }

        [HttpGet]
        [Route("api/products/GetAllProducts")]
        public async Task<ActionResult<IEnumerable<GetProductDto>>> GetAllProducts()
        {
            var Products = await _productServices.GetAllProducts();
            return Ok(Products);
        }

        [HttpPut]
        [Route("api/orders/{ProductId}")]
        [Authorize(Roles = "Admin")]
        public async Task<ActionResult> UpdateStatusOfOrder(int ProductId, CreateProductDto productDto)
        {
            if (ProductId == 0)
            {
                return BadRequest("OrderId mustn't be zero");
            }

            await _productServices.UpdateProduct(ProductId, productDto);
            return Ok();
        }

    }
}
