using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Prueba_productsEF.Models;
using Prueba_ProductsEF.Dtos;
using Prueba_ProductsEF.Models;
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
        return Ok(new APIResponse(true, "Products successfuly found", products));

    }

    
    [HttpGet("hasStock")]
    public async Task<ActionResult<IEnumerable<Product>>> GetStockProducts()
    {
        var products = await _service.GetStockProductsAsync();
        return Ok(new APIResponse(true, "Products with stock successfuly found", products));
    }

    [HttpGet("{id}")]
    public async Task<ActionResult<ProductDto>> GetProduct(int id)
    {
        var prod = await _service.GetProductByIdAsync(id);
        if (prod == null)
            return NotFound(new APIResponse(false, "Product not found"));
        return Ok(new APIResponse(true, "Product successfuly found", prod));
    }

    [HttpGet("{id}/stock")]
    public async Task<ActionResult<ProductDto>> GetStockProduct(int id)
    {
        var stockProd = await _service.GetStockProductByIdAsync(id);
        return Ok(new APIResponse(true, "Product with stock successfuly found", stockProd));
    }

    [HttpPost]
    public async Task<ActionResult<ProductDto>> AddProduct(ProductDto prod)
    {
        var newProd = await _service.AddProductAsync(prod);
        return Ok(new APIResponse(true, "Product successfuly added", newProd));
    }


    [HttpPut("{id}")]
    public async Task<IActionResult> UpdateProduct(int id, ProductDto inputProduct)
    {
        var updateProd = await _service.UpdateProductAsync(id, inputProduct);
        if (updateProd == null)
            return NotFound(new APIResponse(false, "Product not found"));

        return Ok(new APIResponse(true, "Product successfuly updated", updateProd));
    }

    [HttpDelete("{id}")]
    public async Task<IActionResult> DeleteProduct(int id)
    {
        var delProd = await _service.DeleteProductAsync(id);
        if (!delProd)
            return NotFound(new APIResponse(false, "Product not found"));

        return Ok(new APIResponse(true, "Product successfuly deleted"));
    }
}