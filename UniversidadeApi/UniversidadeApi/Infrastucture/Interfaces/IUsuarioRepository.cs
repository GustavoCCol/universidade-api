using UniversidadeApi.DTOs;
using UniversidadeApi.Models;

namespace UniversidadeApi.Infrastucture.Interfaces
{
    public interface IUsuarioRepository : IBaseRepository<Usuario>
    {
        Task<string> Login(LoginDTO login_info);
    }
}
