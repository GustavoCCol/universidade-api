using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using UniversidadeApi.DTOs;
using UniversidadeApi.Infrastucture.Context;
using UniversidadeApi.Infrastucture.Interfaces;
using UniversidadeApi.Models;

namespace UniversidadeApi.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class UsuarioController : ControllerBase
    {
        private readonly IUsuarioRepository _usuarioRepository;

        public UsuarioController(IUsuarioRepository usuarioRepository)
        {
            _usuarioRepository = usuarioRepository ?? throw new ArgumentNullException(nameof(usuarioRepository));
        }

        [HttpPost]
        public IActionResult Adicionar([FromBody] UsuarioDTO usuario_recebido)
        {
            var usuario_adicionado = new Usuario
            {
                CPF = usuario_recebido.Cpf,
                NOME = usuario_recebido.Nome,
                SENHA = usuario_recebido.Senha,
                ID = usuario_recebido.Id
            };

            _usuarioRepository.Adicionar(usuario_adicionado);

            return Ok(usuario_adicionado);
        }

        [HttpPut("{id}")]
        public IActionResult Atualizar([FromBody] UsuarioDTO usuario_recebido, int id)
        {
            var usuario_atualizado = new Usuario
            {
                CPF = usuario_recebido.Cpf,
                NOME = usuario_recebido.Nome,
                SENHA = usuario_recebido.Senha,
                ID = id
            };

            _usuarioRepository.Atualizar(usuario_atualizado);

            return Ok(usuario_atualizado);
        }

        [HttpDelete("{id}")]
        public IActionResult Deletar(int id)
        {
            var usuario_deletado = _usuarioRepository.Obter(id);

            _usuarioRepository.Deletar(usuario_deletado);

            return Ok(usuario_deletado);
        }
        [Authorize]
        [HttpGet]
        public IActionResult ObterTodos()
        {
            return Ok(_usuarioRepository.ObterTodos());
        }

        [HttpGet("{id}")]
        public IActionResult Obter(int id)
        {
            return Ok(_usuarioRepository.Obter(id));
        }

        [HttpPost("login")]
        public IActionResult Login([FromBody]LoginDTO login_info)
        {
            var token = _usuarioRepository.Login(login_info);
            if (string.IsNullOrEmpty(token)) Unauthorized("Senha ou cpf incorreto");
            return Ok(new {token});
        }
    }
}
