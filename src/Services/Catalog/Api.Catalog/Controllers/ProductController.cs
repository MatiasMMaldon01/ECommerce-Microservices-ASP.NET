using Application.Catalog.DTO_s;
using Application.Catalog.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace Api.Catalog.Controllers
{
    [ApiController]
    [Route("api/v1/[controller]")]
    public class ProductController : Controller
    {
        private readonly IProductService _productService;
        private readonly ILogger<ProductController> _logger;

        public ProductController(IProductService productSercvice, ILogger<ProductController> logger)
        {
            _productService = productSercvice;
            _logger = logger;
        }

        [HttpPost]
        public IActionResult Create(ProductDTO product)
        {
            _productService.Create(product);

            return Ok("Product created succesfully");
        }

        [HttpPut]
        public IActionResult Update(ProductDTO product)
        {
            _productService.Update(product);

            return Ok("Product updated succesfully");
        }

        [HttpDelete("{id:length(24)}")]
        public IActionResult Delete(string id)
        {
            _productService.Delete(id);

            return Ok("Product deleted succesfully");
        }

        [HttpGet("{id:length(24)}")]
        public IActionResult GetById(string id)
        {
            var product = _productService.GetById(id);

            if (product == null)
            {
                _logger.LogError($"Product with id: {id}, not found.");
                return NotFound();
            }
            return Ok(product);
        }

        [HttpGet]
        public IActionResult Get(string? stringToFind)
        {
            var products = _productService.GetByFilter(stringToFind);

            if (products.Count() == 0)
            {
                _logger.LogError($"Oppsss... there are nothing here...");
                return NotFound();
            }
            return Ok(products);
        }
    }
}
