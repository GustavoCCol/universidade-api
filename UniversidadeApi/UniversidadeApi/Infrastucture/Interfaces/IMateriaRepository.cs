using System.Runtime.Intrinsics.X86;
using UniversidadeApi.Models;

namespace UniversidadeApi.Infrastucture.Interfaces
{
    public interface IMateriaRepository
    {
        //Métodos da matéria

        //(GET) matérias
        List<Materia> GetAll();

        //(GET) matéria por id
        Materia Get(int id);

        //(POST) adicionar uma matéria
        void Add(Materia materia);

        //(PUT) atualizar informações da matéria
        void Update(Materia materia);

        //(DELETE) deletar matéria da universidade
        void Delete(Materia materia);
    }
}
