using System.ComponentModel.DataAnnotations;
using System.Text.Json.Serialization;

namespace WebApiUmg.Models
{
    public class Docente
    {
        [Key]
        public int DocenteId { get; set; }

        [Required]
        [StringLength(100)]
        public string Nombres { get; set; } = string.Empty;

        [Required]
        [StringLength(100)]
        public string Apellidos { get; set; } = string.Empty;

        [JsonIgnore]
        public ICollection<Asignacion>? Asignaciones { get; set; }
    }
}
