using Prueba_productsEF.Models;
using Prueba_ProductsEF.Dtos;
using Prueba_ProductsEF.Models;

public interface ICategoryService
{
    Task<IEnumerable<CategoryDto>> GetCategoriesAsync();
    Task<CategoryDto?> GetCategoryByIdAsync(int id);
    Task<CategoryDto?> GetCategoryByNameAsync(string name);
    Task<CategoryDto?> AddCategoryAsync(CategoryDto categoryDto);
    Task<CategoryDto?> UpdateCategoryAsync(CategoryDto categoryDto);
    Task<bool> DeleteCategoryAsync(int id);
}

namespace Prueba_ProductsEF.Services
{
    public class CategoryService : ICategoryService
    {
        private readonly ICategoryRepository _repo;

        public CategoryService(ICategoryRepository repo)
        {
            _repo = repo;
        }

        public async Task<IEnumerable<CategoryDto>> GetCategoriesAsync()
        {
            var categories = await _repo.GetCategoriesAsync();
            return categories.Select(c => new CategoryDto
            {
                Id = c.Id,
                Name = c.Name
            });
        }

        public async Task<CategoryDto?> GetCategoryByIdAsync(int id)
        {
            var category = await _repo.GetCategoryByIdAsync(id);
            if (category == null)
                throw new Exception("La categoría no existe.");
            return new CategoryDto
            {
                Id = category.Id,
                Name = category.Name
            };
        }

        public async Task<CategoryDto?> GetCategoryByNameAsync(string name)
        {
            var category = await _repo.GetCategoryByNameAsync(name);
            if (category == null)
                throw new Exception("La categoría no existe.");

            return new CategoryDto
            {
                Id = category.Id,
                Name = category.Name
            };
        }

        public async Task<CategoryDto?> AddCategoryAsync(CategoryDto categoryDto)
        {
            var category = new Category
            {
                Name = categoryDto.Name
            };

            await _repo.AddCategoryAsync(category);

            return new CategoryDto
            {
                Id = category.Id,
                Name = category.Name
            };
        }

        public async Task<CategoryDto?> UpdateCategoryAsync(CategoryDto categoryDto)
        {
            var category = await _repo.GetCategoryByIdAsync(categoryDto.Id);
            if (category == null)
            {
                throw new Exception("La categoría no existe.");
            }

            category.Id = categoryDto.Id;
            category.Name = categoryDto.Name;

            await _repo.UpdateCategoryAsync(category);

            return new CategoryDto
            {
                Id = category.Id,
                Name = category.Name
            };

        }

        public async Task<bool> DeleteCategoryAsync(int id)
        {
            var category = await _repo.GetCategoryByIdAsync(id);
            if (category == null)
            {
                throw new Exception("La categoría no existe.");
            }

            await _repo.DeleteCategoryAsync(id);
            return true;
        }
    }
}
