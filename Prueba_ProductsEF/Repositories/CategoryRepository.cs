using Microsoft.EntityFrameworkCore;
using Prueba_productsEF.Contexts;
using Prueba_ProductsEF.Models;

public interface ICategoryRepository
{
    Task<IEnumerable<Category>> GetCategoriesAsync();
    Task<Category?> GetCategoryByIdAsync(int id);
    Task<Category?> GetCategoryByNameAsync(string name);
    Task AddCategoryAsync(Category category);
    Task UpdateCategoryAsync(Category category);
    Task DeleteCategoryAsync(int id);
}

namespace Prueba_ProductsEF.Repositories
{
    public class CategoryRepository : ICategoryRepository
    {
        private readonly ProductDb _db;
        public CategoryRepository(ProductDb db)
        {
            _db = db;
        }
        public async Task<IEnumerable<Category>> GetCategoriesAsync()
        {
            return await _db.Categories.ToListAsync();
        }
        public async Task<Category?> GetCategoryByIdAsync(int id)
        {
            return await _db.Categories.FindAsync(id);
        }

        public async Task<Category?> GetCategoryByNameAsync(string name)
        {
            return await _db.Categories.FirstOrDefaultAsync(c => c.Name == name);
        }

        public async Task AddCategoryAsync(Category category)
        {
            _db.Categories.Add(category);
            await _db.SaveChangesAsync();
        }
        public async Task UpdateCategoryAsync(Category category)
        {
            _db.Categories.Update(category);
            await _db.SaveChangesAsync();
        }
        public async Task DeleteCategoryAsync(int id)
        {
            var category = await _db.Categories.FindAsync(id);
            if (category != null)
            {
                _db.Categories.Remove(category);
                await _db.SaveChangesAsync();
            }
        }
    }
}
