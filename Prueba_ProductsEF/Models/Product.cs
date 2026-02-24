using Prueba_ProductsEF.Models;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Prueba_productsEF.Models
{
    public class Product
    {
        [Key]
        public int Id { get; set; }

        public string Name { get; set; } = "";

        public string Description { get; set; } = "";

        [Column(TypeName = "decimal(18,2)")]
        public decimal Price { get; set; }

        public string ImageLink { get; set; } = "";

        public int CategoryId { get; set; } = 1;
        public Category Category { get; set; }

        public ICollection<StockStore> StockStores { get; set; }
    }

}
