using System.ComponentModel.DataAnnotations;

namespace AeropuertoConlara.Models
{
    public class FormularioCIVSL
    {
        [Key]
        public int Id { get; set; }

        [Required]
        public string Nombre { get; set; }

        [Required]
        public int Edad { get; set; }

        [Required]
        public string Localidad { get; set; }

        [Required]
        [EmailAddress]
        public string Email { get; set; }

        public string Telefono { get; set; }

        [Required]
        public string NivelEstudios { get; set; }

        public string Comentarios { get; set; }

        public DateTime FechaEnvio { get; set; } = DateTime.Now;
    }
}
