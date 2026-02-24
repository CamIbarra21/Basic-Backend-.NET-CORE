using Microsoft.EntityFrameworkCore;
using Prueba_productsEF.Contexts;
using Prueba_ProductsEF.Models;

public interface IStoreRepository
{
    Task<IEnumerable<Store>> GetStoresAsync();
    Task<Store?> GetStoreByIdAsync(int id);
    Task AddStoreAsync(Store store);
    Task UpdateStoreAsync(Store store);
    Task DeleteStoreAsync(int id);
}

namespace Prueba_ProductsEF.Repositories
{
    public class StoreRepository : IStoreRepository
    {
        private readonly ProductDb _db;

        public StoreRepository(ProductDb db)
        {
            _db = db;
        }

        public async Task<IEnumerable<Store>> GetStoresAsync()
        {
            return await _db.Stores.ToListAsync();
        }

        public async Task<Store?> GetStoreByIdAsync(int id)
        {
            return await _db.Stores.FindAsync(id);
        }

        public async Task AddStoreAsync(Store store)
        {
            _db.Stores.Add(store);
            await _db.SaveChangesAsync();
        }

        public async Task UpdateStoreAsync(Store store)
        {
            _db.Stores.Update(store);
            await _db.SaveChangesAsync();
        }

        public async Task DeleteStoreAsync(int id)
        {
            var store = await _db.Stores.FindAsync(id);
            if (store != null)
            {
                _db.Stores.Remove(store);
                await _db.SaveChangesAsync();
            }
        }
    }
}
