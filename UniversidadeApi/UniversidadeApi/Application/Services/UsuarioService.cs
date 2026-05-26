using Serilog;
using System.Linq.Expressions;
using UniversidadeApi.DTOs;
using UniversidadeApi.Infrastucture.Context;
using UniversidadeApi.Infrastucture.Interfaces;
using UniversidadeApi.Models;

namespace UniversidadeApi.Application.Services
{
    public class UsuarioService : IBaseRepository<Usuario>, IUsuarioRepository
    {
        private readonly IBaseRepository<Usuario> _repositoryUsuario;

        public UsuarioService(IBaseRepository<Usuario> repositoryUsuario)
        {
            _repositoryUsuario = repositoryUsuario;
        }

        public Task Adicionar(Usuario entity)
        {
            return _repositoryUsuario.Adicionar(entity);
        }

        public Task Atualizar(Usuario entity)
        {
            return _repositoryUsuario.Atualizar(entity);
        }

        public Task Deletar(int id)
        {
            return _repositoryUsuario.Deletar(id);
        }

        public async Task<string> Login(LoginDTO login_info)
        {
            var usuarios = await _repositoryUsuario.ObterTodos();
            var usuario_bd = usuarios.FirstOrDefault(l => l.CPF == login_info.Cpf && l.SENHA == login_info.Senha);
            if (usuario_bd == null) return "";
            return TokenService.Gerar(usuario_bd);
        }

        public Task<Usuario> ObterPorCondicaoAsync(Expression<Func<Usuario, bool>> predicate, params Expression<Func<Usuario, object>>[] includes)
        {
            return _repositoryUsuario.ObterPorCondicaoAsync(predicate, includes);
        }

        public Task<Usuario> ObterPorId(int id)
        {
            return _repositoryUsuario.ObterPorId(id);
        }

        public Task<List<Usuario>> ObterTodos()
        {
            return _repositoryUsuario.ObterTodos();
        }

        public Task<IQueryable<Usuario>> ObterTodosPorCondicaoAsync(Expression<Func<Usuario, bool>> predicate, params Expression<Func<Usuario, object>>[] includes)
        {
            return _repositoryUsuario.ObterTodosPorCondicaoAsync(predicate, includes);
        }
    }
}
