using Microsoft.AspNetCore.Mvc;
using Prueba_ProductsEF.Dtos;
using Prueba_ProductsEF.Models;

namespace Prueba_ProductsEF.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class CategoriesController : ControllerBase
    {
        private readonly ICategoryService _service;

        public CategoriesController(ICategoryService service)
        {
            _service = service;
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<CategoryDto>>> GetCategories()
        {
            var categories = await _service.GetCategoriesAsync();
            return Ok(new APIResponse(true, "Categories successfuly found", categories));
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<CategoryDto>> GetCategory(int id)
        {
            var category = await _service.GetCategoryByIdAsync(id);
            if (category == null)
                return NotFound(new APIResponse(false, "Category not found"));
            return Ok(new APIResponse(true, "Category successfuly found", category));
        }

        [HttpPost]
        public async Task<ActionResult<CategoryDto>> AddCategory(CategoryDto categoryDto)
        {
            var newCategory = await _service.AddCategoryAsync(categoryDto);
            return Ok(new APIResponse(true, "Category successfuly added", newCategory));
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateCategory(int id, CategoryDto categoryDto)
        {
            var updateCategory = await _service.UpdateCategoryAsync(id, categoryDto);
            if (updateCategory == null)
                return NotFound(new APIResponse(false, "Category not found"));
            return Ok(new APIResponse(true, "Category successfuly updated", updateCategory));
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteCategory(int id)
        {
            var deleted = await _service.DeleteCategoryAsync(id);
            if (!deleted)
                return NotFound(new APIResponse(false, "Category not found"));
            return Ok(new APIResponse(true, "Category successfuly deleted"));
        }
    }
}
