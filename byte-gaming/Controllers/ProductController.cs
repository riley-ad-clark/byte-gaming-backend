using AutoMapper;
using BLL;
using Entities.Entities;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace byte_gaming.Controllers
{
    [Route("api/product-controller")]
    [ApiController]
    public class ProductController : ControllerBase
    {

        private readonly IProductService _productService;
        private readonly IMapper _mapper;

        public ProductController(IProductService productService, IMapper mapper)
        {
            _productService = productService;
            _mapper = mapper;
        }

        [HttpGet]
        [Route("/products")]
        public async Task<IActionResult> GetAllProducts()
        {
            List<Product> products = await _productService.GetAllProductsService();

            if (products.Count == 0)
            {
                return NotFound("No products were found, try adding some!");
            } else
            {
                return Ok(products);
            }
        }

        [HttpGet]
        [Route("/product/{slug}")]
        public async Task<IActionResult> GetProductBySlug(string slug)
        {
            var product = await _productService.GetProductBySlugService(slug);

            if (product == null)
            {
                return NotFound($"No products with the identifier of {slug} were found");
            } else
            {
                return Ok(product);
            }
        }

        [HttpPost]
        [Route("/product/add")]
        public async Task<IActionResult> AddProduct(ProductDTO product)
        {
            if (product.ProductPrice <= 0 || product.ProductPrice == null)
            {
                return BadRequest("Please enter a value great than 0");
            } else if (product.ProductName == null || product.ProductName.Length == 0)
            {
                return BadRequest("Please enter a valid product name");
            } else if (product.ProductDescription == null || product.ProductDescription.Length == 0)
            {
                return BadRequest("Please enter a valid product description");
            } else if (product.ProductImageURL == null || product.ProductImageURL.Length == 0)
            {
                return BadRequest("Please enter a valid image URL");
            } else if (product.ProductSlug == null || product.ProductSlug.Length == 0 || product.ProductSlug.Contains(' '))
            {
                return BadRequest("Please enter a valid product slug");
            }
            else
            {
                var response = await _productService.AddProductService(product);
                if (response != false)
                {
                    return Ok("Product successfully added!");
                } else
                {
                    return NotFound("Something went wrong internally, please try again!");
                }
            }
        }

        [HttpDelete]
        [Route("/product/remove/{id}")]
        public async Task<IActionResult> DeleteProduct(int id)
        {
            var response = await _productService.DeleteProductService(id);

            if (response != false)
            {
                return Ok($"Product with the id of {id} was successfully deleted!");
            } else
            {
                return NotFound($"No product with the id of {id} was found");
            }
        }

        [HttpPut]
        [Route("/product/edit")]
        public async Task<IActionResult> EditProduct(Product product)
        {
            var response = await _productService.EditProductService(product);

            if (response != false)
            {
                return Ok($"Product with the id of {product.ProductId} was successfully updated to have the" +
                          $" following variables: " +
                          $"{product.ProductName}," +
                          $" {product.ProductDescription}," +
                          $" {product.ProductPrice}," +
                          $" and {product.ProductImageURL},"
                          );
            } else
            {
                return NotFound($"No product with the id of {product.ProductId} was found");
            }
        }
    }
}
