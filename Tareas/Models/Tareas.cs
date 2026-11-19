using System.ComponentModel.DataAnnotations;

namespace Tareas.Models
{


        public enum EstadoTarea
        {
            Pendiente,
            EnProgreso,
            Completada
        }

        public class Tarea
        {
            public int Id { get; set; }

            [Required(ErrorMessage = "El título es obligatorio")]
            [StringLength(200, MinimumLength = 1)]
            public string Titulo { get; set; } = string.Empty;

            [StringLength(1000)]
            public string? Descripcion { get; set; }

            [Required]
            public EstadoTarea Estado { get; set; } = EstadoTarea.Pendiente;

            [Required(ErrorMessage = "El responsable es obligatorio")]
            [StringLength(100)]
            public string Responsable { get; set; } = string.Empty;

            [Required(ErrorMessage = "La fecha límite es obligatoria")]
            [DataType(DataType.Date)]
            public DateTime FechaLimite { get; set; }
        }




}
