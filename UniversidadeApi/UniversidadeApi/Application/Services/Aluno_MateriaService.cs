using UniversidadeApi.DTOs;
using Microsoft.EntityFrameworkCore;
using UniversidadeApi.Infrastucture.Interfaces;
using UniversidadeApi.Models;
using Serilog;
using Microsoft.AspNetCore.Http.HttpResults;
using System.Linq.Expressions;

namespace UniversidadeApi.Application.Services
{
    public class Aluno_MateriaService : IBaseRepository<Aluno_Materia>, IAluno_MateriaRepository
    {
        private readonly IBaseRepository<Aluno_Materia> _repositoryAlunoMateria;
        private readonly IBaseRepository<Aluno> _repositoryAluno;
        private readonly IBaseRepository<Materia> _repositoryMateria;

        public Aluno_MateriaService(IBaseRepository<Aluno_Materia> repositoryAlunoMateria, IBaseRepository<Aluno> repositoryAluno, IBaseRepository<Materia> repositoryMateria)
        {
            _repositoryAlunoMateria = repositoryAlunoMateria;
            _repositoryAluno = repositoryAluno;
            _repositoryMateria = repositoryMateria;
        }

        public async Task<IEnumerable<Object>> ObterTodosDesc()
        {
            Log.Information("Mostrou todos os alunos cadastrados em disciplinas com desc");
            return await ObterTodosPorCondicaoAsync(n => true, m => m.Materia, a => a.Aluno);
        }
        public async Task<IEnumerable<Object>> ObterCadastrosAlunoPorId(int id)
        {
            var aluno = await _repositoryAluno.ObterPorId(id);
            if (aluno == null)
            {
                Log.Error("Aluno não encontrado");
                return null;
            }
            Log.Information("Mostrou os cadastros via aluno");
            return await ObterTodosPorCondicaoAsync(am => am.ALUNO_ID == id, x => x.Materia);
        }
        public async Task<IEnumerable<Object>> ObterCadastrosMateriaPorId(int id)
        {
            var materia = await _repositoryMateria.ObterPorId(id);
            if (materia == null)
            {
                Log.Error("Matéria não encontrada");
                return null;
            }
            Log.Information("Mostrou os cadastros via matéria");
            return await ObterTodosPorCondicaoAsync(am => am.MATERIA_ID == id, x => x.Aluno);
        }

        public Task<Aluno_Materia> ObterPorId(int id)
        {
            return _repositoryAlunoMateria.ObterPorId(id);
        }

        public Task<List<Aluno_Materia>> ObterTodos()
        {
            return _repositoryAlunoMateria.ObterTodos();
        }

        public Task Adicionar(Aluno_Materia entity)
        {
            return _repositoryAlunoMateria.Adicionar(entity);
        }

        public Task Atualizar(Aluno_Materia entity)
        {
            return _repositoryAlunoMateria.Atualizar(entity);
        }

        public Task Deletar(int id)
        {
            return _repositoryAlunoMateria.Deletar(id);
        }

        public Task<Aluno_Materia> ObterPorCondicaoAsync(Expression<Func<Aluno_Materia, bool>> predicate, params Expression<Func<Aluno_Materia, object>>[] includes)
        {
            return _repositoryAlunoMateria.ObterPorCondicaoAsync(predicate, includes);
        }

        public Task<IQueryable<Aluno_Materia>> ObterTodosPorCondicaoAsync(Expression<Func<Aluno_Materia, bool>> predicate, params Expression<Func<Aluno_Materia, object>>[] includes)
        {
            return _repositoryAlunoMateria.ObterTodosPorCondicaoAsync(predicate, includes);
        }
    }
}
