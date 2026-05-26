using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace UniversidadeApi.Models
{
    [Table("ALUNO", Schema = "ESCOLA")]
    public class Aluno : Base
    {
        public string NOME { get; set; } 
        public string MATRICULA { get; set; }
        public DateTime DATA_NASCIMENTO { get; set; }
        public DateTime DATA_INGRESSO { get; set; }
        public string? CURSO { get; set; }
    }
}
