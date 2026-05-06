using UniversidadeApi.Models;

namespace UniversidadeApi.Infrastucture.Interfaces
{
    public interface INotaRepository
    {
        //Métodos da nota

        //(GET)média das notas dos alunos
        List<Nota> GetAll();

        IEnumerable<Object> GetAllDesc();

        //(GET) nota específica
        Nota Get(int  id);

        //(POST) colocar nota de um aluno numa matéria
        void Add(Nota nota);

        //(PUT) atualizar nota do aluno numa matéria
        void Update(Nota nota);

        //(DELETE) nota
        void Delete(Nota nota);
    }
}
