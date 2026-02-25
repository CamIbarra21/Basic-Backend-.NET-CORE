using Microsoft.EntityFrameworkCore;
using Prueba_productsEF.Contexts;
using Prueba_ProductsEF.Models;

public interface IRolRepository
{
    Task<IEnumerable<Rol>> GetRolsAsync();
    Task<Rol?> GetRolByIdAsync(int id);
    Task<Rol?> GetRolByNameAsync(string name);
    Task AddRolAsync(Rol rol);
    Task UpdateRolAsync(Rol rol);
    Task DeleteRolAsync(int id);
}

namespace Prueba_ProductsEF.Repositories
{
    public class RolRepository : IRolRepository
    {
        private readonly ProductDb _db;

        public RolRepository(ProductDb db)
        {
            _db = db;
        }

        public async Task<IEnumerable<Rol>> GetRolsAsync()
        {
            return await _db.Rols.ToListAsync();
        }
        public async Task<Rol?> GetRolByIdAsync(int id)
        {
            return await _db.Rols.FindAsync(id);
        }

        public async Task<Rol?> GetRolByNameAsync(string name)
        {
            return await _db.Rols.FirstOrDefaultAsync(c => c.Name == name);
        }

        public async Task AddRolAsync(Rol rol)
        {
            _db.Rols.Add(rol);
            await _db.SaveChangesAsync();
        }
        public async Task UpdateRolAsync(Rol rol)
        {
            _db.Rols.Update(rol);
            await _db.SaveChangesAsync();
        }
        public async Task DeleteRolAsync(int id)
        {
            var rol = await _db.Rols.FindAsync(id);
            if (rol != null)
            {
                _db.Rols.Remove(rol);
                await _db.SaveChangesAsync();
            }
        }

    }
}
