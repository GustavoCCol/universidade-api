using UniversidadeApi.DTOs;
using UniversidadeApi.Infrastucture.Interfaces;
using UniversidadeApi.Models;
using Serilog;

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
            return _context.Alunos_Materias.Select(a => new
            {
                aluno_id = a.ALUNO_ID,
                aluno = a.Aluno,
                materia_id = a.MATERIA_ID,
                materia = a.Materia
            }).ToList();
        }

        public Aluno_Materia Get(int id)
        {
            Log.Information("Mostrou um aluno cadastrado em disciplinas");
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
            Log.Information("Atualizou um aluno na materia");
        }
        public void Delete(Aluno_Materia aluno_Materia)
        {
            _context.Remove(aluno_Materia);
            _context.SaveChanges();
            Log.Information("Removeu um aluno da materia");
        }
    }
}
