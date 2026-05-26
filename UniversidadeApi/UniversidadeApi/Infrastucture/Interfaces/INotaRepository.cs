using UniversidadeApi.Application.Services;
using UniversidadeApi.Models;

namespace UniversidadeApi.Infrastucture.Interfaces
{
    public interface INotaRepository : IBaseRepository<Nota>
    {
        Task<bool> AdicionarNota(Nota nota);
        Task<IEnumerable<Object>> ObterTodasDesc();
        Task<IEnumerable<Object>> ObterPorBimestre(int bimestre, int aluno_id);
        Task<IEnumerable<Object>> ObterPorMateria(int materia_id, int aluno_id);
    }
}
