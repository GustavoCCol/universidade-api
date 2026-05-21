using UniversidadeApi.DTOs;
using UniversidadeApi.Models;

namespace UniversidadeApi.Infrastucture.Interfaces
{
    public interface IUsuarioRepository
    {
        List<Usuario> ObterTodos();
        Usuario Obter(int id);
        void Adicionar(Usuario usuario);
        void Atualizar(Usuario usuario);
        void Deletar(Usuario usuario);
        string Login(LoginDTO login_info);
    }
}
