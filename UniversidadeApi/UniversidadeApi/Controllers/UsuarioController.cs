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
        [Authorize]
        [HttpPost]
        public async Task<IActionResult> Adicionar([FromBody] UsuarioDTO usuario_recebido)
        {
            var usuario_adicionado = new Usuario
            {
                CPF = usuario_recebido.Cpf,
                NOME = usuario_recebido.Nome,
                SENHA = usuario_recebido.Senha,
                ID = usuario_recebido.Id
            };

            await _usuarioRepository.Adicionar(usuario_adicionado);

            return Ok(usuario_adicionado);
        }
        [Authorize]
        [HttpPut("{id}")]
        public async Task<IActionResult> Atualizar([FromBody] UsuarioDTO usuario_recebido, int id)
        {
            var usuario_atualizado = new Usuario
            {
                CPF = usuario_recebido.Cpf,
                NOME = usuario_recebido.Nome,
                SENHA = usuario_recebido.Senha,
                ID = id
            };

            await _usuarioRepository.Atualizar(usuario_atualizado);

            return Ok(usuario_atualizado);
        }
        [Authorize]
        [HttpDelete("{id}")]
        public async Task<IActionResult> Deletar(int id)
        {
            await _usuarioRepository.Deletar(id);
            return Ok();
        }
        [Authorize]
        [HttpGet]
        public async Task<IActionResult> ObterTodos()
        {
            return Ok(await _usuarioRepository.ObterTodos());
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> Obter(int id)
        {
            return Ok(await _usuarioRepository.ObterPorId(id));
        }

        [HttpPost("login")]
        public async Task<IActionResult> Login([FromBody]LoginDTO login_info)
        {
            var token = await _usuarioRepository.Login(login_info);
            if (string.IsNullOrEmpty(token)) Unauthorized("Senha ou cpf incorreto");
            return Ok(new {token});
        }
    }
}
