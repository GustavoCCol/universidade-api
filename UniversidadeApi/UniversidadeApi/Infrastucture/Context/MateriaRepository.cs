using Serilog;
using UniversidadeApi.Infrastucture.Interfaces;
using UniversidadeApi.Models;

namespace UniversidadeApi.Infrastucture.Context
{
    public class MateriaRepository : IMateriaRepository
    {
        private readonly ConnectionContext _context;

        public MateriaRepository(ConnectionContext context)
        {
            _context = context;
        }
        public void Add(Materia materia)
        {
            _context.Materias.Add(materia);
            _context.SaveChanges();
            Log.Information("Adicionou uma matéria");
        }

        public void Delete(Materia materia)
        {
            _context.Materias.Remove(materia);
            _context.SaveChanges();
            Log.Information("Removeu uma matéria");
        }

        public List<Materia> GetAll()
        {
            Log.Information("Mostrou todas as matérias");
            return _context.Materias.ToList();
        }

        public Materia Get(int id)
        {
            var materia = _context.Materias.Find(id);
            Log.Information("Mostrou uma matéria");
            return materia;
        }

        public void Update(Materia materia)
        {
            var materia_antiga = _context.Materias.Find(materia.ID);
            {
                materia_antiga.PROFESSOR = materia.PROFESSOR;
                materia_antiga.NOME = materia.NOME;
            }
            _context.SaveChanges();
            Log.Information("Atualizou uma matéria");
        }
    }
}
