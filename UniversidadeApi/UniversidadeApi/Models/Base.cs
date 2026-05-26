using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace UniversidadeApi.Models
{
    public class Base
    {
        [Key]
        [Column("ID")]
        public int ID { get; set; }
    }
}
