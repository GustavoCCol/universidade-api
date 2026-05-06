namespace UniversidadeApi.DTOs
{
    public class NotaDTO
    {
        public int Id { get; set; }
        public int Materia_id { get; set; }
        public int Aluno_id { get; set; }
        public int Bimestre { get; set; }
        public float Nota { get; set; }
    }
}
