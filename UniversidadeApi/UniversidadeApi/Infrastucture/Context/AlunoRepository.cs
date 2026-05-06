using Microsoft.EntityFrameworkCore;
using UniversidadeApi.Infrastucture.Interfaces;
using UniversidadeApi.Models;
using Serilog;

namespace UniversidadeApi.Infrastucture.Context
{
    public class AlunoRepository : IAlunoRepository
    {
        private readonly ConnectionContext _context;

        public AlunoRepository(ConnectionContext context)
        {
            _context = context;
        }
        public void Add(Aluno aluno)
        {
            _context.Alunos.Add(aluno);
            _context.SaveChanges();
            Log.Information("Adicionou um aluno");
        }

        public void Delete(Aluno aluno)
        {
            _context.Alunos.Remove(aluno);
            _context.SaveChanges();
            Log.Information("Deletou um aluno aluno");
        }

        public List<Aluno> GetAll()
        {
            Log.Information("Mostrou todos os alunos");
            return _context.Alunos.ToList();
        }
        
        public Aluno Get(int id)
        {
            Log.Information("Mostrou um aluno");
            return _context.Alunos.Find(id);
            
        }

        public void Update(Aluno aluno_novo)
        {
            var aluno_antigo = _context.Alunos.Find(aluno_novo.ID);
            {
                aluno_antigo.NOME = aluno_novo.NOME;
                aluno_antigo.MATRICULA = aluno_novo.MATRICULA;
                aluno_antigo.DATA_NASCIMENTO = aluno_novo.DATA_NASCIMENTO;
                aluno_antigo.DATA_INGRESSO = aluno_novo.DATA_INGRESSO;
                aluno_antigo.CURSO = aluno_novo.CURSO;
            }
            _context.SaveChanges();
            Log.Information("Atualizou um aluno");
        }
    }
}
