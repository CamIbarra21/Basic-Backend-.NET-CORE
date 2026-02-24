using Prueba_productsEF.Models;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Prueba_ProductsEF.Models
{
    public class Category
    {
        [Key]
        public int Id { get; set; }
        public string Name { get; set; }

        // Relación: una categoría tiene muchos productos
        public ICollection<Product> Products { get; set; }
    }
}
