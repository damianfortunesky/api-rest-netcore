using System.ComponentModel.DataAnnotations;

namespace api_rest_netcore.Models
{
    public class Contact
    {

        [Key]
        public int ContactId { get; set; }

        [Required]
        [StringLength(100, ErrorMessage = "El nombre no puede superar los 100 caracteres.")]
        public required string ContactName { get; set; }

        [Required]
        public required string Description { get; set; }

        public DateTime CreatedAt { get; set; } = DateTime.UtcNow;
    }
}
