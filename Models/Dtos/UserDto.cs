using System.ComponentModel.DataAnnotations;

namespace api_rest_netcore.Models.Dtos
{
    public class UserDto
    {

        // El UserDto se usa para exponer datos básicos de un usuario al frontend o a la API. No incluir propiedades sensibles como Password.
        public int Id { get; set; }

        public required string Username { get; set; } 

        public required string Email { get; set; }

        public int Role { get; set; }

        public DateTime CreatedAt { get; set; } 

        public bool IsActive { get; set; }
    }
}
