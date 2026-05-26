using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace UniversidadeApi.Models
{
    [Table("MATERIA", Schema = "ESCOLA")]
    public class Materia : Base
    {
        public string? PROFESSOR { get; set; }
        public string? NOME { get; set; }
    }
}
