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
        public List<Aluno_Materia> GetAll()
        {
            Log.Information("Mostrou todos os alunos cadastrados em disciplinas");
            return _context.Alunos_Materias.ToList();
        }

        public IEnumerable<Object> GetAllDesc()
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
        public IEnumerable<Object> GetCadastrosAlunoById(int id)
        {
            var aluno = _context.Alunos.Find(id);
            if (aluno == null) return null;
            var cadastros_aluno = _context.Alunos_Materias
                /*.Where(am => am.ALUNO_ID == id)
                .Include(b => b.Materia)
                .Include(c => c.Aluno)*/
                .Where(am => am.ALUNO_ID == id)
                .Select(a => new
                {
                    Materia = a.Materia,
                    Aluno = a.Aluno
                })
                .ToList();
            return cadastros_aluno;
        }
        public IEnumerable<Object> GetCadastrosMateriaById(int id)
        {
            var materia = _context.Materias.Find(id);
            if (materia == null) return null;
            var cadastros_materias = _context.Alunos_Materias
                .Where(am => am.MATERIA_ID == id)
                .Select(a => new
                {
                    Aluno = a.Aluno,
                    Materia = a.Materia
                }).ToList();
            return cadastros_materias;
        }
        public Aluno_Materia Get(int id)
        {
            Log.Information("Mostrou um cadastro");
            return _context.Alunos_Materias.Find(id);
        }
        public Aluno GetAluno(int id)
        {
            Log.Information("Procurou um aluno");
            return _context.Alunos.Find(id);
        }

        public Materia GetMateria(int id)
        {
            Log.Information("Procurou uma matéria");
            return _context.Materias.Find(id);
        }

        public void Add(Aluno_Materia aluno_Materia)
        {
            _context.Alunos_Materias.Add(aluno_Materia);
            _context.SaveChanges();
            Log.Information("Cadastrou um aluno em uma matéria");
        }

        public void Update(Aluno_Materia aluno_Materia_novo)
        {
            var aluno_Materia_antigo = _context.Alunos_Materias.Find(aluno_Materia_novo.ID);
            {
                aluno_Materia_antigo.MATERIA_ID = aluno_Materia_novo.MATERIA_ID;
                aluno_Materia_antigo.ALUNO_ID = aluno_Materia_novo.ALUNO_ID;
            }
            _context.SaveChanges();
            Log.Information("Atualizou um aluno numa materia");
        }
        public void Delete(Aluno_Materia aluno_Materia)
        {
            _context.Remove(aluno_Materia);
            _context.SaveChanges();
            Log.Information("Removeu um aluno da materia");
        }
    }
}
