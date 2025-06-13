using System.ComponentModel.DataAnnotations;

namespace AeropuertoConlara.Models
{
    public class Usuario
    {
        [Key]
        public int Id { get; set; }

        [Required]
        public string Nombre { get; set; }

        [Required]
        [EmailAddress]
        public string Email { get; set; }

        [Required]
        public string ClaveHash { get; set; }

        public string Rol { get; set; }
    }
}
