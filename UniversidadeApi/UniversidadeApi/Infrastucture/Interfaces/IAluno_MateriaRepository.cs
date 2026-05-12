using UniversidadeApi.Models;
using UniversidadeApi.DTOs;

namespace UniversidadeApi.Infrastucture.Interfaces
{
    public interface IAluno_MateriaRepository
    {
        List<Aluno_Materia> ObterTodos();
        IEnumerable<Object> ObterTodosDesc(); 
        IEnumerable<Object> ObterCadastrosAlunoPorId(int id);
        IEnumerable<Object> ObterCadastrosMateriaPorId(int id);
        Aluno_Materia Obter(int id);
        void Adicionar(Aluno_Materia aluno_Materia);
        void Atualizar(Aluno_Materia aluno_Materia);
        void Deletar(Aluno_Materia aluno_Materia);
    }
}
