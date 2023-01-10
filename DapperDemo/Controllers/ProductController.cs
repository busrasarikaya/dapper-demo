using DapperDemo.Entities;
using DapperDemo.Interfaces;
using DapperDemo.Repositories;
using Microsoft.AspNetCore.Mvc;

namespace DapperDemo.Controllers
{

    [ApiController]
    [Route("api/products")]
    public class ProductController : Controller
    {
        private readonly IProductRepository _productRepository;

        public ProductController(IProductRepository productRepository)
        {
            _productRepository = productRepository;
        }

        [HttpGet]
        public IActionResult GetProducts()
        {
            try
            {
                var products = _productRepository.GetProducts();
                return Ok(products);
            }
            catch (Exception exception)
            {
                return BadRequest(exception.Message);
            }
        }

        [HttpGet("{productId}", Name = "GetProductById")]
        public IActionResult GetProductById(int productId)
        {
            try
            {
                var product = _productRepository.GetProductById(productId);

                return product == null ? NotFound() : Ok(product);
            }
            catch (Exception exception)
            {
                return BadRequest(exception.Message);
            }
        }

        [HttpPost]
        public IActionResult CreateProduct(ProductDTO product)
        {
            try
            {
                _productRepository.CreateProduct(product);
                return Ok();
            }
            catch (Exception exception)
            {
                return BadRequest(exception.Message);
            }
        }

        [HttpPut("{productId}")]
        public IActionResult UpdateProduct(int productId, ProductDTO product)
        {
            try
            {
                var existingProduct = _productRepository.GetProductById(productId);
                if (existingProduct == null)
                {
                    return NotFound();
                }

                _productRepository.UpdateProduct(productId, product);
                return Ok();
            }
            catch (Exception exception)
            {
                return BadRequest(exception.Message);
            }
        }

        [HttpDelete("{productId}")]
        public IActionResult DeleteProduct(int productId)
        {
            try
            {
                var existingProduct = _productRepository.GetProductById(productId);
                if (existingProduct == null)
                {
                    return NotFound();
                }

                _productRepository.DeleteProduct(productId);
                return Ok();
            }
            catch (Exception exception)
            {
                return BadRequest(exception.Message);
            }
        }

        [HttpGet("GetProductsByCategoryId/{categoryId}")]
        public IActionResult GetCompanyForEmployee(int categoryId)
        {
            try
            {
                var products = _productRepository.GetProductsByCategoryId(categoryId);
                return products == null ? NotFound() : Ok(products);
            }
            catch (Exception exception)
            {
                return BadRequest(exception.Message);
            }
        }
    }
}







