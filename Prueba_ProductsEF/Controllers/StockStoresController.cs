using Microsoft.AspNetCore.Mvc;
using Prueba_ProductsEF.Dtos;
using Prueba_ProductsEF.Models;

namespace Prueba_ProductsEF.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class StockStoresController : ControllerBase
    {
        private readonly IStockStoreService _service;

        public StockStoresController(IStockStoreService service)
        {
            _service = service;
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<StockStoreDto>>> GetStockStores()
        {
            var stockStores = await _service.GetStockStoresAsync();
            return Ok(new APIResponse(true, "StockStores successfuly found", stockStores));
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<StockStoreDto>> GetStockStore(int id)
        {
            var stockStore = await _service.GetStockStoreByIdAsync(id);
            if (stockStore == null)
                return NotFound(new APIResponse(false, "StockStore not found"));
            return Ok(new APIResponse(true, "StockStore successfuly found", stockStore));
        }

        [HttpPost]
        public async Task<ActionResult<StockStoreDto>> AddStockStore(StockStoreDto stockStoreDto)
        {
            var newStockStore = await _service.AddStockStoreAsync(stockStoreDto);
            return Ok(new APIResponse(true, "StockStore successfuly added", newStockStore));
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateStockStore(int id, StockStoreDto stockStoreDto)
        {
            var updateStockStore = await _service.UpdateStockStoreAsync(id, stockStoreDto);
            if (updateStockStore == null)
                return NotFound(new APIResponse(false, "StockStore not found"));
            return Ok(new APIResponse(true, "StockStore successfuly updated", updateStockStore));
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteStockStore(int id)
        {
            var deleted = await _service.DeleteStockStoreAsync(id);
            if (!deleted)
                return NotFound(new APIResponse(false, "StockStore not found"));
            return Ok(new APIResponse(true, "StockStore successfuly deleted"));
        }
    }
}
