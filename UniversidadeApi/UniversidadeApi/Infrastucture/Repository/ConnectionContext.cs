using Microsoft.EntityFrameworkCore;
using UniversidadeApi.Models;
using Oracle.ManagedDataAccess.Types;
using Oracle.ManagedDataAccess.Client;

namespace UniversidadeApi.Infrastucture.Context
{
    public class ConnectionContext : DbContext
    {
        public DbSet<Aluno> Alunos { get; set; }
        public DbSet<Aluno_Materia> Alunos_Materias { get; set; }
        public DbSet<Materia> Materias { get; set; }
        public DbSet<Nota> Notas { get; set; }
        public DbSet<Usuario> Usuarios { get; set; }
        public ConnectionContext(DbContextOptions<ConnectionContext> options) : base(options) { }
    }
}
