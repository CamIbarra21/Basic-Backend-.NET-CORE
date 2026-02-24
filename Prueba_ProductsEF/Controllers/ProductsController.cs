using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Prueba_ProductsEF.Dtos;
using static Microsoft.EntityFrameworkCore.DbLoggerCategory;

[ApiController]
[Route("api/[controller]")]
public class ProductsControllerEF : ControllerBase
{
    private readonly IProductService _service;

    public ProductsControllerEF(IProductService service)
    {
        _service = service;
    }

    [HttpGet]
    public async Task<ActionResult<IEnumerable<ProductDto>>> GetProducts()
    {
        var products = await _service.GetProductsAsync();
        return Ok(products);

    }

    /*
    [HttpGet("complete")]
    public async Task<ActionResult<IEnumerable<Product>>> GetCompleteProducts()
    {
        return await _db.Products.Where(p => p.HasStock).ToListAsync();
    }*/

    [HttpGet("{id}")]
    public async Task<ActionResult<ProductDto>> GetProduct(int id)
    {
        var prod = await _service.GetProductByIdAsync(id);
        if (prod == null)
            return NotFound(new { message = "Producto no encontrado" });
        return Ok(prod);
    }

    [HttpPost]
    public async Task<ActionResult<ProductDto>> AddProduct(ProductDto prod)
    {
        try
        {
            var newProd = await _service.AddProductAsync(prod);
            return CreatedAtAction(nameof(GetProduct), new { id = newProd.Id }, newProd);
        }
        catch (Exception ex)
        {
            return BadRequest(new { message = ex.Message });
        }
    }

    [HttpPut("{id}")]
    public async Task<IActionResult> UpdateProduct(int id, ProductDto inputProduct)
    {
        try
        {
            var updateProd = await _service.UpdateProductAsync(id, inputProduct);
            if (updateProd == null)
                return NotFound(new { message = "Producto no encontrado" });

            return Ok(updateProd);

        }
        catch (Exception ex)
        {
            return BadRequest(new { message = ex.Message });
        }
    }

    [HttpDelete("{id}")]
    public async Task<IActionResult> DeleteProduct(int id)
    {
        try
        {
            var delProd = await _service.DeleteProductAsync(id);
            if (!delProd)
                return NotFound(new { message = "Producto no encontrado" });

            return NoContent();

        }
        catch (Exception ex)
        {
            return BadRequest(new { message = ex.Message });
        }
    }
}