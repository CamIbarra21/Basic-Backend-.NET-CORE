using Prueba_productsEF.Models;
using System.ComponentModel.DataAnnotations;

namespace Prueba_ProductsEF.Models
{
    public class Rol
    {
        [Key]
        public int Id { get; set; }
        public string Name { get; set; }
        public ICollection<User> Users { get; set; }

    }
}
