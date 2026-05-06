using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace UniversidadeApi.Models
{
    [Table("ALUNO_MATERIA", Schema = "UNIVERSIDADE")]
    public class Aluno_Materia
    {
        [Key]
        public int ID { get; set; }
        [ForeignKey("MATERIA_ID")]
        public int MATERIA_ID { get; set; }
        [ForeignKey("ALUNO_ID")]
        public int ALUNO_ID { get; set; }
        public Aluno Aluno { get; set; }
        public Materia Materia { get; set; }
    }
}
