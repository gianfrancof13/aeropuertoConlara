using System.ComponentModel.DataAnnotations;

namespace AeropuertoConlara.Models
{
    public class Usuario
    {
        public int Id { get; set; }

        [Required(ErrorMessage = "El nombre de usuario es obligatorio")]
        [StringLength(50, ErrorMessage = "El nombre de usuario no puede tener más de 50 caracteres")]
        public string NombreUsuario { get; set; } = string.Empty;

        [Required(ErrorMessage = "La contraseña es obligatoria")]
        [StringLength(100, ErrorMessage = "La contraseña no puede tener más de 100 caracteres")]
        public string Password { get; set; } = string.Empty;

        [Required(ErrorMessage = "El rol es obligatorio")]
        public string Rol { get; set; } = string.Empty;

        public DateTime FechaCreacion { get; set; } = DateTime.Now;
    }
}
