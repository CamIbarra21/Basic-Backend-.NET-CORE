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
    }
}
