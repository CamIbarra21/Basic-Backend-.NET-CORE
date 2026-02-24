using Prueba_productsEF.Models;
using System.ComponentModel.DataAnnotations;

namespace Prueba_ProductsEF.Models
{
    public class StockStore
    {
        [Key]
        public int Id { get; set; }
        public int ProductId { get; set; }
        public int StoreId { get; set; }
        public int Quantity { get; set; }

        // Relaciones
        public Product Product { get; set; }
        public Store Store { get; set; }

    }
}
