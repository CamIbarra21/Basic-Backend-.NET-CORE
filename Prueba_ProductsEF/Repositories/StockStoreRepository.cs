using Microsoft.EntityFrameworkCore;
using Prueba_productsEF.Contexts;
using Prueba_ProductsEF.Models;

public interface IStockStoreRepository
{
    Task<IEnumerable<StockStore>> GetStockStoresAsync();
    Task<StockStore?> GetStockStoreByIdAsync(int id);
    Task AddStockStoreAsync(StockStore stockStore);
    Task UpdateStockStoreAsync(StockStore stockStore);
    Task DeleteStockStoreAsync(int id);
}

namespace Prueba_ProductsEF.Repositories
{
    public class StockStoreRepository : IStockStoreRepository
    {
        private readonly ProductDb _db;
        public StockStoreRepository(ProductDb db)
        {
            _db = db;
        }
        public async Task<IEnumerable<StockStore>> GetStockStoresAsync()
        {
            return await _db.StockStores.Include(ss => ss.Product).Include(ss => ss.Store).ToListAsync();
        }
        public async Task<StockStore?> GetStockStoreByIdAsync(int id)
        {
            return await _db.StockStores.Include(ss => ss.Product).Include(ss => ss.Store).FirstOrDefaultAsync(ss => ss.Id == id);
        }
        public async Task AddStockStoreAsync(StockStore stockStore)
        {
            _db.StockStores.Add(stockStore);
            await _db.SaveChangesAsync();
        }
        public async Task UpdateStockStoreAsync(StockStore stockStore)
        {
            _db.StockStores.Update(stockStore);
            await _db.SaveChangesAsync();
        }
        public async Task DeleteStockStoreAsync(int id)
        {
            var stockStore = await _db.StockStores.FindAsync(id);
            if (stockStore != null)
            {
                _db.StockStores.Remove(stockStore);
                await _db.SaveChangesAsync();
            }
        }
    }
}
