namespace UniversidadeApi.DTOs
{
    public class AlunoDTO
    {
        public int Id { get; set; }
        public string Nome { get; set; }
        public string Matricula { get; set; }
        public string Nascimento {  get; set; }
        public string Ingresso {  get; set; }
        public string? Curso { get; set; }
    }
}
