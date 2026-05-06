using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace UniversidadeApi.Models
{
    [Table("MATERIA", Schema = "UNIVERSIDADE")]
    public class Materia
    {
        [Key]
        public int ID { get; set; }
        public string? PROFESSOR { get; set; }
        public string? NOME { get; set; }
    }
}
