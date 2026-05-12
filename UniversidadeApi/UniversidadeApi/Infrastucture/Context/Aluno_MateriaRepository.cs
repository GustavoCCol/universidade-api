using UniversidadeApi.DTOs;
using Microsoft.EntityFrameworkCore;
using UniversidadeApi.Infrastucture.Interfaces;
using UniversidadeApi.Models;
using Serilog;
using Microsoft.AspNetCore.Http.HttpResults;

namespace UniversidadeApi.Infrastucture.Context
{
    public class Aluno_MateriaRepository : IAluno_MateriaRepository
    {
        private readonly ConnectionContext _context;

        public Aluno_MateriaRepository(ConnectionContext context)
        {
            _context = context;
        }
        public List<Aluno_Materia> ObterTodos()
        {
            Log.Information("Mostrou todos os alunos cadastrados em disciplinas");
            return _context.Alunos_Materias.ToList();
        }

        public IEnumerable<Object> ObterTodosDesc()
        {
            Log.Information("Mostrou todos os alunos cadastrados em disciplinas com desc");
            return _context.Alunos_Materias.Select(am => new
            {
                aluno_id = am.ALUNO_ID,
                aluno = am.Aluno,
                materia_id = am.MATERIA_ID,
                materia = am.Materia
            }).ToList();
        }
        public IEnumerable<Object> ObterCadastrosAlunoPorId(int id)
        {
            var aluno = _context.Alunos.FirstOrDefault(a => a.ID == id);
            if (aluno == null)
            {
                Log.Error("Aluno não encontrado");
                return null;
            }
            var cadastros_aluno = _context.Alunos_Materias
                .Where(am => am.ALUNO_ID == id)
                .Select(a => new
                {
                    materia_id = a.Materia.ID,
                    materia_nome = a.Materia.NOME,
                    materia_professor = a.Materia.PROFESSOR
                })
                .ToList();
            Log.Information("Mostrou os cadastros via aluno");
            return cadastros_aluno;
        }
        public IEnumerable<Object> ObterCadastrosMateriaPorId(int id)
        {
            var materia = _context.Materias.FirstOrDefault(m => m.ID == id);
            if (materia == null)
            {
                Log.Error("Matéria não encontrada");
                return null;
            }
            var cadastros_materias = _context.Alunos_Materias
                .Where(am => am.MATERIA_ID == id)
                .Select(a => new
                {
                    aluno_nome = a.Aluno.NOME,
                    aluno_id = a.Aluno.ID,
                    aluno_curso = a.Aluno.CURSO,
                    materia_nome = a.Materia.NOME
                }).ToList();
            Log.Information("Mostrou os cadastros via matéria");
            return cadastros_materias;
        }
        public Aluno_Materia Obter(int id)
        {
            Log.Information("Mostrou um cadastro");
            return _context.Alunos_Materias.Find(id);
        }
        public Aluno ObterAluno(int id)
        {
            Log.Information("Procurou um aluno");
            return _context.Alunos.Find(id);
        }

        public Materia ObterMateria(int id)
        {
            Log.Information("Procurou uma matéria");
            return _context.Materias.Find(id);
        }

        public void Adicionar(Aluno_Materia aluno_Materia)
        {
            _context.Alunos_Materias.Add(aluno_Materia);
            _context.SaveChanges();
            Log.Information("Cadastrou um aluno em uma matéria");
        }

        public void Atualizar(Aluno_Materia aluno_Materia_novo)
        {
            var aluno_Materia_antigo = _context.Alunos_Materias.Find(aluno_Materia_novo.ID);
            {
                aluno_Materia_antigo.MATERIA_ID = aluno_Materia_novo.MATERIA_ID;
                aluno_Materia_antigo.ALUNO_ID = aluno_Materia_novo.ALUNO_ID;
            }
            _context.SaveChanges();
            Log.Information("Atualizou um aluno numa materia");
        }
        public void Deletar(Aluno_Materia aluno_Materia)
        {
            _context.Remove(aluno_Materia);
            _context.SaveChanges();
            Log.Information("Removeu um aluno da materia");
        }
    }
}
