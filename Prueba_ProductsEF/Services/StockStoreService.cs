using Prueba_ProductsEF.Dtos;
using Prueba_ProductsEF.Models;

public interface IStockStoreService
{
    Task<IEnumerable<StockStoreDto>> GetStockStoresAsync();
    Task<StockStoreDto?> GetStockStoreByIdAsync(int id);
    Task<StockStoreDto?> AddStockStoreAsync(StockStoreDto stockStoreDto);
    Task<StockStoreDto?> UpdateStockStoreAsync(int id, StockStoreDto stockStoreDto);
    Task<bool> DeleteStockStoreAsync(int id);
}

namespace Prueba_ProductsEF.Services
{
    public class StockStoreService : IStockStoreService
    {
        private readonly IStockStoreRepository _repo;
        private readonly IStoreRepository _repoStore;
        private readonly IProductRepository _repoProduct;

        public StockStoreService(IStockStoreRepository repo, IStoreRepository repoStore, IProductRepository repoProduct)
        {
            _repo = repo;
            _repoStore = repoStore;
            _repoProduct = repoProduct;
        }

        public async Task<IEnumerable<StockStoreDto>> GetStockStoresAsync()
        {
            var stockStores = await _repo.GetStockStoresAsync();

            return stockStores.Select(static ss => new StockStoreDto
            {
                Id = ss.Id,
                ProductId = ss.ProductId,
                ProductName = ss.Product.Name,
                StoreId = ss.StoreId,
                StoreName = ss.Store.Name,
                Quantity = ss.Quantity
            });
        }

        public async Task<StockStoreDto?> GetStockStoreByIdAsync(int id)
        {
            var stockStore = await _repo.GetStockStoreByIdAsync(id);
            if (stockStore == null)
                throw new Exception("El stock para la tienda no existe");

            return new StockStoreDto
            {
                Id = stockStore.Id,
                ProductId = stockStore.ProductId,
                ProductName = stockStore.Product.Name,
                StoreId = stockStore.StoreId,
                StoreName = stockStore.Store.Name,
                Quantity = stockStore.Quantity
            };
        }

        public async Task<StockStoreDto?> AddStockStoreAsync(StockStoreDto stockStoreDto)
        {
            var stockStore = new StockStore
            {
                StoreId = stockStoreDto.StoreId,
                ProductId = stockStoreDto.ProductId,
                Quantity = stockStoreDto.Quantity
            };

            await _repo.AddStockStoreAsync(stockStore);

            var savedStockStore = await _repo.GetStockStoreByIdAsync(stockStore.Id);

            return new StockStoreDto
            {
                Id = savedStockStore.Id,
                StoreId = savedStockStore.StoreId,
                StoreName = savedStockStore.Store.Name,
                ProductId = savedStockStore.ProductId,
                ProductName = savedStockStore.Product.Name,
                Quantity = savedStockStore.Quantity
            };
        }

        public async Task<StockStoreDto?> UpdateStockStoreAsync(int id, StockStoreDto stockStoreDto)
        {
            var stockStore = await _repo.GetStockStoreByIdAsync(id);
            if (stockStore == null)
                throw new Exception("El stock para la tienda no existe");

            stockStore.StoreId = stockStoreDto.StoreId;
            stockStore.ProductId = stockStoreDto.ProductId;
            stockStore.Quantity = stockStoreDto.Quantity;

            await _repo.UpdateStockStoreAsync(stockStore);

            return new StockStoreDto
            {
                Id = stockStore.Id,
                StoreId = stockStore.StoreId,
                StoreName = stockStore.Store.Name,
                ProductId = stockStore.ProductId,
                ProductName = stockStore.Product.Name,
                Quantity = stockStore.Quantity
            };
        }

        public async Task<bool> DeleteStockStoreAsync(int id)
        {
            var stockStore = await _repo.GetStockStoreByIdAsync(id);
            if (stockStore == null)
                throw new Exception("El stock para la tienda no existe");

            await _repo.DeleteStockStoreAsync(id);

            return true;
        }
    }
}
