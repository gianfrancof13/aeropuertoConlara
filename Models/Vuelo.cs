using System.ComponentModel.DataAnnotations;

namespace AeropuertoConlara.Models
{
    public class Vuelo
    {
        public int Id { get; set; }

        [Required(ErrorMessage = "La fecha es obligatoria")]
        [Display(Name = "Fecha")]
        public DateTime Fecha { get; set; }

        [Required(ErrorMessage = "El número de vuelo es obligatorio")]
        [Display(Name = "Número de Vuelo")]
        public string NumeroVuelo { get; set; }

        [Required(ErrorMessage = "La hora de arribo es obligatoria")]
        [Display(Name = "Hora de Arribo")]
        public TimeSpan HoraArribo { get; set; }

        [Required(ErrorMessage = "La hora de partida es obligatoria")]
        [Display(Name = "Hora de Partida")]
        public TimeSpan HoraPartida { get; set; }

        [Required(ErrorMessage = "La ruta es obligatoria")]
        [Display(Name = "Ruta")]
        public string Ruta { get; set; }

        [Required(ErrorMessage = "El equipo es obligatorio")]
        [Display(Name = "Equipo")]
        public string Equipo { get; set; }

        [Required(ErrorMessage = "El TA es obligatorio")]
        [Display(Name = "TA")]
        public string TA { get; set; }
    }
}
