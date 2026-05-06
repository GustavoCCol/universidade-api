using UniversidadeApi.Models;

namespace UniversidadeApi.Infrastucture.Interfaces

{
    public interface IAlunoRepository
    {
        //Métodos dos alunos

        //(GET)Alunos matriculados na universidade
        List<Aluno> GetAll();

        //(GET)Aluno com matricula
        Aluno Get(int id);

        //(POST) colocar um aluno novo na universidade
        void Add(Aluno aluno);

        //(PUT) atualizar informações do aluno
        void Update(Aluno aluno_novo);

        //(DELETE) aluno trancou a faculdade
        void Delete(Aluno aluno);

    }
}
