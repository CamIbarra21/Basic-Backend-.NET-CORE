using System.ComponentModel.DataAnnotations;

namespace Prueba_ProductsEF.Models
{
    public class Store
    {
        [Key]
        public int Id { get; set; }
        public string Name { get; set; }
        public string Location { get; set; }
        public string Manager { get; set; }
        public string OpeningDays { get; set; }
        public string OpeningHours { get; set; }

        // Relación: una tienda tiene muchos stocks
        public ICollection<StockStore> StockStores { get; set; }

    }
}
