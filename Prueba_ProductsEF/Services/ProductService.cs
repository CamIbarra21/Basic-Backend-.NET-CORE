using Prueba_productsEF.Models;
using Prueba_ProductsEF.Dtos;
using Prueba_ProductsEF.Models;

public interface IProductService
{
    Task<IEnumerable<ProductDto>> GetProductsAsync();
    Task<ProductDto?> GetProductByIdAsync(int id);
    Task<IEnumerable<ProductDto>> GetStockProductsAsync();
    Task<int> GetStockProductByIdAsync(int id);
    Task<ProductDto?> AddProductAsync(ProductDto productDto);
    Task<ProductDto?> UpdateProductAsync(int id, ProductDto productDto);
    Task<bool> DeleteProductAsync(int id);
    Task<IEnumerable<ProductDto>> GetProductsPagedAsync(int pageNumber, int pageSize);
    Task<int> GetCountProductsAsync();
}

namespace Prueba_ProductsEF.Services 
{
    public class ProductService : IProductService
    {
        private readonly IProductRepository _repo;
        private readonly ICategoryRepository _repoCategory;

        public ProductService(IProductRepository repo, ICategoryRepository repoCategory)
        {
            _repo = repo;
            _repoCategory = repoCategory;
        }

        public async Task<IEnumerable<ProductDto>> GetProductsAsync()
        {
            var products = await _repo.GetProductsAsync();
            return products.Select(p => new ProductDto
            {
                Id = p.Id,
                Name = p.Name,
                Description = p.Description,
                Price = p.Price,
                HasStock = p.StockStores != null && p.StockStores.Sum(ss => ss.Quantity) > 0,
                ImageLink = p.ImageLink,
                Category = p.Category.Name
            });
        }

        public async Task<ProductDto?> GetProductByIdAsync(int id)
        {
            var product = await _repo.GetProductByIdAsync(id);
            if (product == null)
            {
                throw new Exception("El producto no existe.");
            }

            return new ProductDto
            {
                Id = product.Id,
                Name = product.Name,
                Description = product.Description,
                Price = product.Price,
                HasStock = product.StockStores != null && product.StockStores.Sum(ss => ss.Quantity) > 0,
                ImageLink = product.ImageLink,
                Category = product.Category.Name
            };
        }

        public async Task<IEnumerable<ProductDto>> GetStockProductsAsync()
        {
            var products = await _repo.GetProductsAsync();
            var productsWithStock = products.Where(p => p.StockStores != null && p.StockStores.Sum(ss => ss.Quantity) > 0);

            return productsWithStock.Select(p => new ProductDto
            {
                Id = p.Id,
                Name = p.Name,
                Description = p.Description,
                Price = p.Price,
                HasStock = true,
                ImageLink = p.ImageLink,
                Category = p.Category.Name
            });
        }

        public async Task<int> GetStockProductByIdAsync(int id)
        {
            var product = await _repo.GetProductByIdAsync(id);
            if (product == null)
            {
                throw new Exception("El producto no existe.");
            }

            return product.StockStores != null ? product.StockStores.Sum(ss => ss.Quantity) : 0;
        }

        public async Task<ProductDto?> AddProductAsync(ProductDto productDto)
        {
            var category = await _repoCategory.GetCategoryByNameAsync(productDto.Category);
            if (category == null)
                throw new Exception("La categoría no existe.");

            var product = new Product
            {
                Name = productDto.Name,
                Description = productDto.Description,
                Price = productDto.Price,
                ImageLink = productDto.ImageLink,
                CategoryId = category == null ? 1 : category.Id
            };
            await _repo.AddProductAsync(product);

            return new ProductDto
            {
                Id = product.Id,
                Name = product.Name,
                Description = product.Description,
                HasStock = product.StockStores != null && product.StockStores.Sum(ss => ss.Quantity) > 0,
                ImageLink = product.ImageLink,
                Category = category == null ? "General" : category.Name
            };
        }

        public async Task<ProductDto?> UpdateProductAsync(int id, ProductDto productDto)
        {
            var product = await _repo.GetProductByIdAsync(id);
            if (product == null)
            {
                throw new Exception("El producto no existe.");
            }

            product.Name = productDto.Name;
            product.Description = productDto.Description;
            product.Price = productDto.Price;
            product.ImageLink = productDto.ImageLink;
            product.CategoryId = (await _repoCategory.GetCategoryByNameAsync(productDto.Category))?.Id ?? product.CategoryId;

            await _repo.UpdateProductAsync(product);

            return new ProductDto
            {
                Id = product.Id,
                Name = product.Name,
                Description = product.Description,
                HasStock = product.StockStores != null && product.StockStores.Sum(ss => ss.Quantity) > 0,
                Price = product.Price,
                ImageLink = product.ImageLink,
                Category = (await _repoCategory.GetCategoryByIdAsync(product.CategoryId))?.Name ?? "General"
            };
        }

        public async Task<bool> DeleteProductAsync(int id)
        {
            var product = await _repo.GetProductByIdAsync(id);
            if (product == null)
            {
                throw new Exception("El producto no existe.");
            }

            await _repo.DeleteProductAsync(id);
            return true;

        }

        public async Task<IEnumerable<ProductDto>> GetProductsPagedAsync(int pageNumber, int pageSize)
        {
            var products = await _repo.GetProductsPagedAsync(pageNumber, pageSize);
            return products.Select(p => new ProductDto
            {
                Id = p.Id,
                Name = p.Name,
                Description = p.Description,
                Price = p.Price,
                HasStock = p.StockStores != null && p.StockStores.Sum(ss => ss.Quantity) > 0,
                ImageLink = p.ImageLink,
                Category = p.Category.Name
            });
        }

        public async Task<int> GetCountProductsAsync()
        {
            return await _repo.GetCountProductsAsync();
        }
    }
}
