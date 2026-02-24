using Prueba_ProductsEF.Dtos;
using Prueba_ProductsEF.Models;

public interface IStoreService
{
    Task<IEnumerable<StoreDto>> GetStoresAsync();
    Task<StoreDto?> GetStoreByIdAsync(int id);
    Task<StoreDto?> AddStoreAsync(StoreDto store);
    Task<StoreDto?> UpdateStoreAsync(int id, StoreDto store);
    Task<bool> DeleteStoreAsync(int id);
}

namespace Prueba_ProductsEF.Services
{
    public class StoreService : IStoreService
    {
        private readonly IStoreRepository _repo;

        public StoreService(IStoreRepository repo)
        {
            _repo = repo;
        }

        public async Task<IEnumerable<StoreDto>> GetStoresAsync()
        {
            var stores = await _repo.GetStoresAsync();
            return stores.Select(p => new StoreDto
            {
                Id = p.Id,
                Name = p.Name,
                Location = p.Location,
                Manager = p.Manager,
                OpeningDays = p.OpeningDays.Split(',').ToList(),
                OpeningHours = p.OpeningHours
            });
        }

        public async Task<StoreDto?> GetStoreByIdAsync(int id)
        {
            var store = await _repo.GetStoreByIdAsync(id);
            if (store == null)
            {
                throw new Exception("La categoría no existe.");
            }

            return new StoreDto
            {
                Id = store.Id,
                Name = store.Name,
                Location = store.Location,
                Manager = store.Manager,
                OpeningDays = store.OpeningDays.Split(',').ToList(),
                OpeningHours = store.OpeningHours
            };
        }

        public async Task<StoreDto?> AddStoreAsync(StoreDto storeDto)
        {
            var store = new Store
            {
                Name = storeDto.Name,
                Location = storeDto.Location,
                Manager = storeDto.Manager,
                OpeningDays = string.Join(',', storeDto.OpeningDays),
                OpeningHours = storeDto.OpeningHours,
            };

            await _repo.AddStoreAsync(store);

            return new StoreDto
            {
                Id = store.Id,
                Name = store.Name,
                Location = store.Location,
                Manager = store.Manager,
                OpeningDays = store.OpeningDays.Split(',').ToList(),
                OpeningHours = store.OpeningHours
            };
        }

        public async Task<StoreDto?> UpdateStoreAsync(int id, StoreDto storeDto)
        {
            var store = await _repo.GetStoreByIdAsync(id);
            if (store == null)
            {
                throw new Exception("La categoría no existe.");
            }

            store.Name = storeDto.Name;
            store.Location = storeDto.Location;
            store.Manager = storeDto.Manager;
            store.OpeningDays = string.Join(',', storeDto.OpeningDays);
            store.OpeningHours = storeDto.OpeningHours;

            await _repo.UpdateStoreAsync(store);

            return new StoreDto
            {
                Id = store.Id,
                Name = store.Name,
                Location = store.Location,
                Manager = store.Manager,
                OpeningDays = store.OpeningDays.Split(',').ToList(),
                OpeningHours = store.OpeningHours
            };
        }

        public async Task<bool> DeleteStoreAsync(int id)
        {
            var store = await _repo.GetStoreByIdAsync(id);
            if (store == null)
            {
                throw new Exception("La categoría no existe.");
            }

            await _repo.DeleteStoreAsync(id);
            return true;
        }
    }
}
