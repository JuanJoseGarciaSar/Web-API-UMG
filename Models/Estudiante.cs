using System.ComponentModel.DataAnnotations;
using System.Text.Json.Serialization;

namespace WebApiUmg.Models
{
    public class Estudiante
    {
        [Key]
        public int EstudianteId { get; set; }

        [Required]
        [StringLength(100)]
        public string Nombres { get; set; } = string.Empty;

        [Required]
        [StringLength(100)]
        public string Apellidos { get; set; } = string.Empty;

        [StringLength(250)]
        public string? Direccion { get; set; }

        [JsonIgnore]
        public ICollection<Asignacion>? Asignaciones { get; set; }
    }
}
