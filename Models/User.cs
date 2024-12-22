using System.ComponentModel.DataAnnotations;

namespace api_rest_netcore.Modelos
{
    public class User
    {

        [Key]
        public int Id { get; set; }

        [Required] // Validación en runtime
        [StringLength(100, ErrorMessage = "El nombre de usuario no puede superar los 100 caracteres.")]
        public required string Username { get; set; } // Required Validación en compilación

        [Required]
        [EmailAddress(ErrorMessage = "Debe proporcionar un correo válido.")]
        public required string Email { get; set; }

        [Required] 
        public required string PasswordHash { get; set; } 

        public int Role { get; set; } = 1;

        public DateTime CreatedAt { get; set; } = DateTime.UtcNow;

        public bool IsActive { get; set; } = true;
    }
}
