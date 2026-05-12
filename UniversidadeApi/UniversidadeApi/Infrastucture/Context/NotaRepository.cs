using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Org.BouncyCastle.Crypto.Engines;
using Serilog;
using System.Diagnostics.CodeAnalysis;
using UniversidadeApi.Infrastucture.Interfaces;
using UniversidadeApi.Models;

namespace UniversidadeApi.Infrastucture.Context
{
    public class NotaRepository : INotaRepository
    {
        private readonly ConnectionContext _context;

        public NotaRepository(ConnectionContext context)
        {
            _context = context;
        }
        public bool Adicionar(Nota nota)
        {
            if (!AlunoExiste(nota.ALUNO_ID))
            {
                Log.Error("Aluno incorreto");
                return false;
            }
            if (!MateriaExiste(nota.MATERIA_ID))
            {
                Log.Error("Matéria incorreta");
                return false; 
            }
            if (!CadastroExiste(nota.MATERIA_ID, nota.ALUNO_ID))
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
            _context.Notas.Add(nota);
            _context.SaveChanges();
            Log.Information("Adicionou uma nota");
            return true;
        }

        public void Deletar(Nota nota)
        {
            _context.Notas.Remove(nota);
            _context.SaveChanges();
            Log.Information("Removeu uma nota");
        }

        public List<Nota> ObterTodas()
        {
            Log.Information("Mostrou todas as notas");
            return _context.Notas.ToList();
        }

        public IEnumerable<Object> ObterTodasDesc()
        {
            Log.Information("Mostrou todas as notas com desc");
            return _context.Notas.Select(a => new
            {
                aluno_id = a.ALUNO_ID,
                aluno = a.Aluno,
                materia_id = a.MATERIA_ID,
                materia = a.Materia,
                Nota = a.NOTA,
                Bimestre = a.BIMESTRE
            }).ToList();
        }

        public IEnumerable<Object> ObterPorBimestre(int bimestre, int aluno_id)
        {
            if (!AlunoExiste(aluno_id)) return null;
            var notas_bimestre = _context.Notas
                .Where(n => (n.BIMESTRE == bimestre) && (n.ALUNO_ID == aluno_id))
                .Select(nb => new
                {
                    aluno_nome = nb.Aluno.NOME,
                    materia_nome = nb.Materia.NOME,
                    nota = nb.NOTA,
                    bimestre = nb.BIMESTRE
                }).ToList();
            return notas_bimestre;
        }
        public IEnumerable<Object> ObterPorMateria(int materia_id, int aluno_id)
        {
            if (!AlunoExiste(aluno_id)) return null;
            if (!MateriaExiste(materia_id)) return null;
            if (!CadastroExiste(materia_id, aluno_id)) return null;
            var notas_materia = _context.Notas
                .Where(n => (n.MATERIA_ID == materia_id) && (n.ALUNO_ID == aluno_id))
                .Select(nm => new
                {
                    materia_nome = nm.Materia.NOME,
                    aluno_nome = nm.Aluno.NOME,
                    Nota = nm.NOTA,
                    Bimestre = nm.BIMESTRE
                }).ToList();
            return notas_materia;
        }
        public bool AlunoExiste(int aluno_id)
        {
            return _context.Alunos.FirstOrDefault(a => a.ID == aluno_id) != null;
        }
        public bool MateriaExiste(int materia_id)
        {
            return _context.Materias.FirstOrDefault(m => m.ID == materia_id) != null;
        }
        public bool CadastroExiste(int materia_id,  int aluno_id)
        {
            return _context.Alunos_Materias.Where(c => (c.MATERIA_ID == materia_id) && (c.ALUNO_ID == aluno_id)) != null;
        }

        public Nota Obter(int id)
        {
            Log.Information("Mostrou uma nota");
            return _context.Notas.Find(id);
        }

        public void Atualizar(Nota nota)
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
