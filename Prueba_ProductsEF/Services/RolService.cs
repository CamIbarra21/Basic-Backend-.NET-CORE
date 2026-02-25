
using Prueba_ProductsEF.Dtos;
using Prueba_ProductsEF.Models;

public interface IRolService
{
    Task<IEnumerable<RolDto>> GetRolsAsync();
    Task<RolDto?> GetRolByIdAsync(int id);
    Task<RolDto?> GetRolByNameAsync(string name);
    Task<RolDto?> AddRolAsync(RolDto rolDto);
    Task<RolDto?> UpdateRolAsync(int id, RolDto rolDto);
    Task<bool> DeleteRolAsync(int id);
}

namespace Prueba_ProductsEF.Services
{
    public class RolService : IRolService
    {
        private readonly IRolRepository _repo;

        public RolService(IRolRepository repo)
        {
            _repo = repo;
        }

        public async Task<IEnumerable<RolDto>> GetRolsAsync()
        {
            var rols = await _repo.GetRolsAsync();
            return rols.Select(c => new RolDto
            {
                Id = c.Id,
                Name = c.Name
            });
        }

        public async Task<RolDto?> GetRolByIdAsync(int id)
        {
            var rol = await _repo.GetRolByIdAsync(id);
            if (rol == null)
                throw new Exception("El rol no existe.");
            return new RolDto
            {
                Id = rol.Id,
                Name = rol.Name
            };
        }

        public async Task<RolDto?> GetRolByNameAsync(string name)
        {
            var rol = await _repo.GetRolByNameAsync(name);
            if (rol == null)
                throw new Exception("El rol no existe.");

            return new RolDto
            {
                Id = rol.Id,
                Name = rol.Name
            };
        }

        public async Task<RolDto?> AddRolAsync(RolDto rolDto)
        {
            var rol = new Rol
            {
                Name = rolDto.Name
            };

            await _repo.AddRolAsync(rol);

            return new RolDto
            {
                Id = rol.Id,
                Name = rol.Name
            };
        }

        public async Task<RolDto?> UpdateRolAsync(int id, RolDto rolDto)
        {
            var rol = await _repo.GetRolByIdAsync(id);
            if (rol == null)
            {
                throw new Exception("La categoría no existe.");
            }

            rol.Name = rolDto.Name;

            await _repo.UpdateRolAsync(rol);

            return new RolDto
            {
                Id = rol.Id,
                Name = rol.Name
            };

        }

        public async Task<bool> DeleteRolAsync(int id)
        {
            var rol = await _repo.GetRolByIdAsync(id);
            if (rol == null)
            {
                throw new Exception("El rol no existe.");
            }

            await _repo.DeleteRolAsync(id);
            return true;
        }
    }
}
