using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace UniversidadeApi.Models
{
    [Table("ALUNO", Schema = "UNIVERSIDADE")]
    public class Aluno
    {
        [Key]
        //[Column("ID")]
        public int ID { get; set; }
        public string NOME { get; set; } 
        public string MATRICULA { get; set; }
        public DateTime DATA_NASCIMENTO { get; set; }
        public DateTime DATA_INGRESSO { get; set; }
        public string? CURSO { get; set; }
        //public ICollection<Aluno_Materia> aluno_Materias { get; set; }
    }
}
