using System.ComponentModel.DataAnnotations;

namespace AeropuertoConlara.Models
{
    public class Vuelo
    {
        [Key]
        public int Id { get; set; }

        [Required]
        public string Aerolinea { get; set; }

        [Required]
        public string NumeroVuelo { get; set; }

        [Required]
        public string Destino { get; set; }

        [Required]
        public DateTime FechaHora { get; set; }

        [Required]
        public string Estado { get; set; }

        public DateTime UltimaActualizacion { get; set; } = DateTime.Now;
    }
}
