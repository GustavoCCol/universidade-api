using Serilog;
using System.Diagnostics.CodeAnalysis;
using UniversidadeApi.Infrastucture.Interfaces;
using UniversidadeApi.Models;
//_context.Alunos.Include[EntityFramework].ToList()
//Linq

namespace UniversidadeApi.Infrastucture.Context
{
    public class NotaRepository : INotaRepository
    {
        private readonly ConnectionContext _context;

        public NotaRepository(ConnectionContext context)
        {
            _context = context;
        }
        public void Add(Nota nota)
        {
            _context.Notas.Add(nota);
            _context.SaveChanges();
            Log.Information("Adicionou uma nota");
        }

        public void Delete(Nota nota)
        {
            _context.Notas.Remove(nota);
            _context.SaveChanges();
            Log.Information("Removeu uma nota");
        }

        public List<Nota> GetAll()
        {
            Log.Information("Mostrou todas as notas");
            return _context.Notas.ToList();
        }

        public IEnumerable<Object> GetAllDesc()
        {
            var desc = _context.Notas.Select(a => new
            {
                aluno_id = a.ALUNO_ID,
                aluno = a.Aluno,
                materia_id = a.MATERIA_ID,
                materia = a.Materia
            }).ToList();

            var all = _context.Notas.ToList();
            Log.Information("Mostrou todas as notas com desc");
            return all.Cast<Object>().ToList();
        }

        public Nota Get(int id)
        {
            Log.Information("Mostrou uma nota");
            return _context.Notas.Find(id);
        }

        public void Update(Nota nota)
        {
            var nota_antiga = _context.Notas.Find(nota.ID);
            {
                nota_antiga.MATERIA_ID = nota.MATERIA_ID;
                nota_antiga.ALUNO_ID = nota.ALUNO_ID;
                nota_antiga.BIMESTRE = nota.BIMESTRE;
                nota_antiga.NOTA = nota.NOTA;
            }
            _context.SaveChanges();
            Log.Information("Atualizou uma nota");
        }
    }
}
