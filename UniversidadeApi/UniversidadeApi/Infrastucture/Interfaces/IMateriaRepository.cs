using System.Runtime.Intrinsics.X86;
using UniversidadeApi.Models;

namespace UniversidadeApi.Infrastucture.Interfaces
{
    public interface IMateriaRepository
    {
        List<Materia> ObterTodas();
        Materia Obter(int id);
        void Adicionar(Materia materia);
        void Atualizar(Materia materia);
        void Deletar(Materia materia);
    }
}
