using UniversidadeApi.Models;
using UniversidadeApi.DTOs;

namespace UniversidadeApi.Infrastucture.Interfaces
{
    public interface IAluno_MateriaRepository
    {
        //(GET) alunos matriculados em materia
        List<Aluno_Materia> GetAll();

        IEnumerable<Object> GetAllDesc(); 

        IEnumerable<Object> GetCadastrosAlunoById(int id);

        IEnumerable<Object> GetCadastrosMateriaById(int id);

        //(GET) matrícula específica
        Aluno_Materia Get(int id);

        //(POST) matricular aluno em uma materia
        void Add(Aluno_Materia aluno_Materia);
        //(PUT) atualizar aluno matriculado
        void Update(Aluno_Materia aluno_Materia);
        //(DELETE) tirar aluno de uma materia
        void Delete(Aluno_Materia aluno_Materia);
    }
}
