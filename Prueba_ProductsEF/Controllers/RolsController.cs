using Microsoft.AspNetCore.Mvc;
using Prueba_ProductsEF.Dtos;
using Prueba_ProductsEF.Models;

namespace Prueba_ProductsEF.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class RolsController : ControllerBase
    {
        private readonly IRolService _service;

        public RolsController(IRolService service)
        {
            _service = service;
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<RolDto>>> GetRols()
        {
            var rols = await _service.GetRolsAsync();
            return Ok(new APIResponse(true, "Rols successfuly found", rols));
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<RolDto>> GetRol(int id)
        {
            var rol = await _service.GetRolByIdAsync(id);
            if (rol == null)
                return NotFound(new APIResponse(false, "Rol not found"));
            return Ok(new APIResponse(true, "Rol successfuly found", rol));
        }

        [HttpPost]
        public async Task<ActionResult<RolDto>> AddRol(RolDto rolDto)
        {
            var newRol = await _service.AddRolAsync(rolDto);
            return Ok(new APIResponse(true, "Rol successfuly added", newRol));
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateRol(int id, RolDto rolDto)
        {
            var updateRol = await _service.UpdateRolAsync(id, rolDto);
            if (updateRol == null)
                return NotFound(new APIResponse(false, "Rol not found"));
            return Ok(new APIResponse(true, "Rol successfuly updated", updateRol));
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteRol(int id)
        {
            var deleted = await _service.DeleteRolAsync(id);
            if (!deleted)
                return NotFound(new APIResponse(false, "Rol not found"));
            return Ok(new APIResponse(true, "Rol successfuly deleted"));
        }
    }
}
