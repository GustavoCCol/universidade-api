using UniversidadeApi.Models;

namespace UniversidadeApi.Infrastucture.Interfaces
{
    public interface INotaRepository
    {
        List<Nota> ObterTodas();
        IEnumerable<Object> ObterTodasDesc();
        IEnumerable<Object> ObterPorBimestre(int bimestre, int aluno_id);
        IEnumerable<Object> ObterPorMateria(int materia_id, int aluno_id);
        Nota Obter(int id);
        bool Adicionar(Nota nota);
        void Atualizar(Nota nota);
        void Deletar(Nota nota);
    }
}
