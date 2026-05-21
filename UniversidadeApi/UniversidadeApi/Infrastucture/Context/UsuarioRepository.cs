using Serilog;
using UniversidadeApi.DTOs;
using UniversidadeApi.Infrastucture.Interfaces;
using UniversidadeApi.Models;
using UniversidadeApi.Services;

namespace UniversidadeApi.Infrastucture.Context
{
    public class UsuarioRepository : IUsuarioRepository
    {
        private readonly ConnectionContext _context;

        public UsuarioRepository(ConnectionContext context)
        {
            _context = context;
        }

        public void Adicionar(Usuario usuario)
        {
            _context.Usuarios.Add(usuario);
            _context.SaveChanges();
            Log.Information("Adicionou um usuario");
        }

        public void Deletar(Usuario usuario)
        {
            _context.Usuarios.Remove(usuario);
            _context.SaveChanges();
            Log.Information("Deletou um usuario");
        }

        public List<Usuario> ObterTodos()
        {
            Log.Information("Mostrou todos os usuarios");
            return _context.Usuarios.ToList();
        }

        public Usuario Obter(int id)
        {
            Log.Information("Buscou um usuario");
            return _context.Usuarios.Find(id);
        }

        public void Atualizar(Usuario usuario_novo)
        {
            var usuario_antigo = _context.Usuarios.Find(usuario_novo.ID);
            {
                usuario_antigo.NOME = usuario_novo.NOME;
                usuario_antigo.CPF = usuario_novo.CPF;
                usuario_antigo.SENHA = usuario_novo.SENHA;
            }
            _context.SaveChanges();
            Log.Information("Atualizou um usuario");
        }

        public string Login(LoginDTO login_info)
        {
            var usuario_bd = _context.Usuarios
                .FirstOrDefault(l => l.CPF == login_info.Cpf && l.SENHA == login_info.Senha);
            if (usuario_bd == null) return "";
            return TokenService.Gerar(usuario_bd);
        }
    }
}
