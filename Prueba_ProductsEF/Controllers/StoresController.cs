using Microsoft.AspNetCore.Mvc;
using Prueba_ProductsEF.Dtos;
using Prueba_ProductsEF.Models;

namespace Prueba_ProductsEF.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class StoresController : ControllerBase
    {
        private readonly IStoreService _service;

        public StoresController(IStoreService service)
        {
            _service = service;
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<StoreDto>>> GetStores()
        {
            var stores = await _service.GetStoresAsync();
            return Ok(new APIResponse(true, "Stores successfuly found", stores));
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<StoreDto>> GetStore(int id)
        {
            var store = await _service.GetStoreByIdAsync(id);
            if (store == null)
                return NotFound(new APIResponse(false, "Store not found"));
            return Ok(new APIResponse(true, "Store successfuly found", store));
        }

        [HttpPost]
        public async Task<ActionResult<StoreDto>> AddStore(StoreDto store)
        {
            var newStore = await _service.AddStoreAsync(store);
            return Ok(new APIResponse(true, "Store successfuly added", newStore));
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateStore(int id, StoreDto inputStore)
        {
            var updateStore = await _service.UpdateStoreAsync(id, inputStore);
            if (updateStore == null)
                return NotFound(new APIResponse(false, "Store not found"));
            return Ok(new APIResponse(true, "Store successfuly updated", updateStore));
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteStore(int id)
        {
            var deleted = await _service.DeleteStoreAsync(id);
            if (!deleted)
                return NotFound(new APIResponse(false, "Store not found"));
            return Ok(new APIResponse(true, "Store successfuly deleted"));
        }
    }
}
