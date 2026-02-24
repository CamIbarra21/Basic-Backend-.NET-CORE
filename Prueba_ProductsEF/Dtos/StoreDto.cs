using Prueba_ProductsEF.Models;

namespace Prueba_ProductsEF.Dtos
{
    public class StoreDto
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Location { get; set; }
        public string Manager { get; set; }
        public List<string> OpeningDays { get; set; }
        public string OpeningHours { get; set; }
    }
}
