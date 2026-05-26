using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace UniversidadeApi.Models
{
    [Table("NOTA", Schema = "ESCOLA")]
    public class Nota : Base
    {
        public int MATERIA_ID { get; set; }
        public int ALUNO_ID { get; set; }
        public int BIMESTRE { get; set; }
        public float NOTA { get; set; }
        [ForeignKey("ALUNO_ID")]
        public Aluno Aluno { get; set; }
        [ForeignKey("MATERIA_ID")]
        public Materia Materia { get; set; }
    }
}
