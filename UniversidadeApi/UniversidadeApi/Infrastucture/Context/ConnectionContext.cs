using Microsoft.EntityFrameworkCore;
using UniversidadeApi.Models;
using Oracle.ManagedDataAccess.Types;
using Oracle.ManagedDataAccess.Client;

namespace UniversidadeApi.Infrastucture.Context
{
    public class ConnectionContext : DbContext
    {
        public ConnectionContext(DbContextOptions<ConnectionContext> options) : base(options) { }
        public DbSet<Aluno> Alunos { get; set; }
        public DbSet<Aluno_Materia> Alunos_Materias { get; set; }
        public DbSet<Materia> Materias { get; set; }
        public DbSet<Nota> Notas { get; set; }
        public DbSet<Usuario> Usuarios { get; set; }

        /*protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            string conString = "Data Source=localhost:1521/xepdb1;User ID=system;Password=oracle;Persist Security Info=True; Connect Timeout=3000;";
            OracleConnection con = new OracleConnection();
            con.ConnectionString = conString;
            con.Open();
        }*/

        /*protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            //Modelos para Aluno_materia
            modelBuilder.Entity<Aluno_Materia>()
                .HasOne(a => a.Aluno).WithMany()
                .HasForeignKey(a => a.ALUNO_ID);

            modelBuilder.Entity<Aluno_Materia>()
                .HasOne(a => a.Materia).WithMany()
                .HasForeignKey(a => a.MATERIA_ID);
            
            //Modelos para Nota
            modelBuilder.Entity<Nota>()
                .HasOne(a => a.Aluno).WithMany()
                .HasForeignKey(a => a.ALUNO_ID);

            modelBuilder.Entity<Nota>()
                .HasOne(a => a.Materia).WithMany()
                .HasForeignKey(a => a.MATERIA_ID);
        }*/
    }
}
