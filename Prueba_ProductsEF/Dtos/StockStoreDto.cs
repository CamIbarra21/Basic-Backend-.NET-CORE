using Prueba_productsEF.Models;
using Prueba_ProductsEF.Models;

namespace Prueba_ProductsEF.Dtos
{
    public class StockStoreDto
    {
        public int Id { get; set; }
        public int ProductId { get; set; }
        public string ProductName { get; set; }
        public int StoreId { get; set; }
        public string StoreName { get; set; }
        public int Quantity { get; set; }
    }
}
