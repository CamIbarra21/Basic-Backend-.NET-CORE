using Microsoft.AspNetCore.Mvc;
using Prueba_ProductsEF.Dtos;

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
            var products = await _service.GetCategoriesAsync();
            return Ok(products);
        }
    }
}
