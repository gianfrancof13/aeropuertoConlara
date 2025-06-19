using System.ComponentModel.DataAnnotations;

namespace AeropuertoConlara.Models
{
    public class FormularioCIVSL
    {
        [Key]
        public int Id { get; set; }

        [Required]
        public string Nombre { get; set; } = string.Empty;

        [Required]
        public int Edad { get; set; }

        [Required]
        public string Localidad { get; set; } = string.Empty;

        [Required]
        [EmailAddress]
        public string Email { get; set; } = string.Empty;

        public string Telefono { get; set; } = string.Empty;

        [Required]
        public string NivelEstudios { get; set; } = string.Empty;

        public string Comentarios { get; set; } = string.Empty;

        public DateTime FechaEnvio { get; set; } = DateTime.Now;
    }
}
