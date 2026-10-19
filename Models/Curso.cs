using System.ComponentModel.DataAnnotations;
using System.Text.Json.Serialization;

namespace WebApiUmg.Models
{
    public class Curso
    {
        [Key]
        public int CursoId { get; set; }

        [Required]
        [StringLength(100)]
        public string Nombre { get; set; } = string.Empty;

        [JsonIgnore]
        public ICollection<Asignacion>? Asignaciones { get; set; }
    }
}
