using UniversidadeApi.Models;

namespace UniversidadeApi.Infrastucture.Interfaces

{
    public interface IAlunoRepository
    {
        List<Aluno> ObterTodos();
        Aluno Obter(int id);
        void Adicionar(Aluno aluno);
        void Atualizar(Aluno aluno_novo);
        void Deletar(Aluno aluno);

    }
}
