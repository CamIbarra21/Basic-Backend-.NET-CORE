using Prueba_ProductsEF.Models;
using System.ComponentModel.DataAnnotations.Schema;

namespace Prueba_ProductsEF.Dtos
{
    public class ProductDto
    {
        public int Id { get; set; }

        public string Name { get; set; } = "";

        public string Description { get; set; } = "";

        public bool HasStock { get; set; }

        [Column(TypeName = "decimal(18,2)")]
        public decimal Price { get; set; }

        public string ImageLink { get; set; } = "";

        public string Category { get; set; }
    }
}
