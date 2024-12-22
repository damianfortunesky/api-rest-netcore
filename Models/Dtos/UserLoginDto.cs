using System.ComponentModel.DataAnnotations;

namespace api_rest_netcore.Models.Dtos
{
    public class UserLoginDto
    {
        [Required(ErrorMessage = "El nombre de usuario es obligatorio.")]
        public required string Username { get; set; }

        [Required(ErrorMessage = "La contraseña es obligatoria.")]
        public required string Password { get; set; }
    }
}
