using UniversidadeApi.Models;
using UniversidadeApi.DTOs;

namespace UniversidadeApi.Infrastucture.Interfaces
{
    public interface IAluno_MateriaRepository : IBaseRepository<Aluno_Materia>
    {
        Task<IEnumerable<Object>> ObterTodosDesc(); 
        Task<IEnumerable<Object>> ObterCadastrosAlunoPorId(int id);
        Task<IEnumerable<Object>> ObterCadastrosMateriaPorId(int id);
    }
}
