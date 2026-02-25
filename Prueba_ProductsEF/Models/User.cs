using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Prueba_ProductsEF.Models
{
    public class User
    {
        [Key]
        public int Id { get; set; }

        public string Fullname { get; set; } = "";

        public string Username { get; set; } = "";

        public string Email { get; set; } = "";

        public string Password { get; set; }

        public string ProfileImage{ get; set; } = "";

        public bool IsDeleted { get; set; } = false;

        public int RolId { get; set; } = 1;

        public Rol Rol { get; set; }

    }
}
