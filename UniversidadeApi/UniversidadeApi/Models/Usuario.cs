using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace UniversidadeApi.Models
{
    [Table("USUARIO", Schema = "ESCOLA")]
    public class Usuario
    {
        [Key]
        public int ID { get; set; }
        public string NOME { get; set; }
        public string CPF { get; set; }
        public string SENHA { get; set; }
    }
}
