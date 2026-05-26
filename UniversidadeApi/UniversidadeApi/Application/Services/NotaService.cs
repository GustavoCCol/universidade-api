using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Org.BouncyCastle.Crypto.Engines;
using Serilog;
using System.Diagnostics.CodeAnalysis;
using System.Linq.Expressions;
using UniversidadeApi.Infrastucture.Context;
using UniversidadeApi.Infrastucture.Interfaces;
using UniversidadeApi.Models;

namespace UniversidadeApi.Application.Services
{
    public class NotaService : IBaseRepository<Nota>, INotaRepository
    {
        private readonly IBaseRepository<Nota> _repositoryNota;
        private readonly IBaseRepository<Aluno_Materia> _repositoryAlunoMateria;
        private readonly IBaseRepository<Aluno> _repositoryAluno;
        private readonly IBaseRepository<Materia> _repositoryMateria;

        public NotaService(
            IBaseRepository<Nota> repositoryNota,
            IBaseRepository<Aluno_Materia> repositoryAlunoMateria,
            IBaseRepository<Aluno> repositoryAluno,
            IBaseRepository<Materia> repositoryMateria
            )
        {
            _repositoryNota = repositoryNota;
            _repositoryAlunoMateria = repositoryAlunoMateria;
            _repositoryAluno = repositoryAluno;
            _repositoryMateria = repositoryMateria;
        }
        public async Task<bool> AdicionarNota(Nota nota)
        {
            if (!(await AlunoExiste(nota.ALUNO_ID)))
            {
                Log.Error("Aluno incorreto");
                return false;
            }
            if (!(await MateriaExiste(nota.MATERIA_ID)))
            {
                Log.Error("Matéria incorreta");
                return false;
            }
            if (!(await CadastroExiste(nota.MATERIA_ID, nota.ALUNO_ID)))
            {
                Log.Error("Cadastro incorreto, matéria ou aluno não cadastrados");
                return false;
            }
            if (nota.NOTA < 0 || nota.NOTA > 10)
            {
                Log.Error("Nota com valor incorreto");
                return false;
            }
            if (!(Enumerable.Range(1, 4).Contains(nota.BIMESTRE)))
            {
                Log.Error("Bimestre com valor incorreto");
                return false;
            }
            await _repositoryNota.Adicionar(nota);
            Log.Information("Adicionou uma nota");
            return true;
        }

        public async Task<IEnumerable<Object>> ObterTodasDesc()
        {
            Log.Information("Mostrou todas as notas com desc");
            return await ObterTodosPorCondicaoAsync(n => true, m => m.Materia, a => a.Aluno);
        }

        public async Task<IEnumerable<Object>> ObterPorBimestre(int bimestre, int aluno_id)
        {
            if (!(await AlunoExiste(aluno_id))) return null;
            return await ObterTodosPorCondicaoAsync(n => (n.BIMESTRE == bimestre) && (n.ALUNO_ID == aluno_id), m => m.Materia, a => a.Aluno);
        }
        public async Task<IEnumerable<Object>> ObterPorMateria(int materia_id, int aluno_id)
        {
            if (!(await AlunoExiste(aluno_id))) return null;
            if (!(await MateriaExiste(materia_id))) return null;
            if (!(await CadastroExiste(materia_id, aluno_id))) return null;
            return await ObterTodosPorCondicaoAsync(n => (n.MATERIA_ID == materia_id) && (n.ALUNO_ID == aluno_id), m => m.Materia, a => a.Aluno);
        }
        public async Task<bool> AlunoExiste(int aluno_id)
        {
            var alunos = await _repositoryAluno.ObterTodos();
            return alunos.FirstOrDefault(a => a.ID == aluno_id) != null;
        }
        public async Task<bool> MateriaExiste(int materia_id)
        {
            var materias = await _repositoryMateria.ObterTodos();
            return materias.FirstOrDefault(m => m.ID == materia_id) != null;
        }
        public async Task<bool> CadastroExiste(int materia_id,  int aluno_id)
        {
            var alun_mat = await _repositoryAlunoMateria.ObterTodos();
            return alun_mat.Where(c => (c.MATERIA_ID == materia_id) && (c.ALUNO_ID == aluno_id)) != null;
        }

        public Task<Nota> ObterPorId(int id)
        {
            return _repositoryNota.ObterPorId(id);
        }

        public Task<List<Nota>> ObterTodos()
        {
            return _repositoryNota.ObterTodos();
        }

        public Task Adicionar(Nota entity)
        {
            return _repositoryNota.Adicionar(entity);
        }

        public Task Atualizar(Nota entity)
        {
            return _repositoryNota.Atualizar(entity);
        }

        public Task Deletar(int id)
        {
            return _repositoryNota.Deletar(id);
        }

        public Task<Nota> ObterPorCondicaoAsync(Expression<Func<Nota, bool>> predicate, params Expression<Func<Nota, object>>[] includes)
        {
            return _repositoryNota.ObterPorCondicaoAsync(predicate, includes);
        }

        public Task<IQueryable<Nota>> ObterTodosPorCondicaoAsync(Expression<Func<Nota, bool>> predicate, params Expression<Func<Nota, object>>[] includes)
        {
            return _repositoryNota.ObterTodosPorCondicaoAsync(predicate, includes);
        }
    }
}
