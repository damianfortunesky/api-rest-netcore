using System.ComponentModel.DataAnnotations;

namespace api_rest_netcore.Models.Dtos
{
    public class UserRegisterDto
    {
        [Required(ErrorMessage = "El nombre de usuario es obligatorio.")]
        [StringLength(100, ErrorMessage = "El nombre de usuario no puede superar los 100 caracteres.")]
        public required string Username { get; set; }

        [Required(ErrorMessage = "El email es obligatorio.")]
        [EmailAddress(ErrorMessage = "Debe proporcionar un correo válido.")]
        public required string Email { get; set; }

        [Required(ErrorMessage = "La contraseña es obligatoria.")]
        [StringLength(100, MinimumLength = 8, ErrorMessage = "La contraseña debe tener al menos 8 caracteres.")]
        public required string Password { get; set; }
    }
}
