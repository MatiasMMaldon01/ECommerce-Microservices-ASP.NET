using Application.Catalog.DTO_s;
using Application.Catalog.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace Api.Catalog.Controllers
{
    [ApiController]
    [Route("api/v1/[controller]")]
    public class CategoryController : Controller
    {
        private readonly ICategoryService _categoryService;
        private readonly ILogger<CategoryController> _logger;

        public CategoryController(ICategoryService categoryService, ILogger<CategoryController> logger)
        {
            _categoryService = categoryService;
            _logger = logger;
        }

        [HttpPost]
        public IActionResult Create(CategoryDTO category)
        {
            _categoryService.Create(category);

            return Ok("Category created succesfully");
        }

        [HttpPut]
        public IActionResult Update(CategoryDTO category)
        {
            _categoryService.Update(category);

            return Ok("Category updated succesfully");
        }

        [HttpDelete("{id:length(24)}")]
        public IActionResult Delete(string id)
        {
            _categoryService.Delete(id);

            return Ok("Category deleted succesfully");
        }

        [HttpGet("{id:length(24)}")]
        public IActionResult GetById(string id)
        {
            var category = _categoryService.GetById(id);

            if (category == null)
            {
                _logger.LogError($"Category with id: {id}, not found.");
                return NotFound();
            }
            return Ok(category);
        }

        [HttpGet]
        public IActionResult Get(string? stringToFind)
        {
            var categories = _categoryService.GetByFilter(stringToFind);

            if (categories == null)
            {
                _logger.LogError($"Oppsss... there are nothing here...");
                return NotFound();
            }
            return Ok(categories);
        }
    }
}
