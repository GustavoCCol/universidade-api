using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Serilog;
using System.Globalization;
using UniversidadeApi.DTOs;
using UniversidadeApi.Infrastucture.Interfaces;
using UniversidadeApi.Models;

namespace UniversidadeApi.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    [Authorize]
    public class AlunoController : ControllerBase
    {
        private readonly IAlunoRepository _alunoRepository;

        public AlunoController(IAlunoRepository alunoRepository)
        {
            _alunoRepository = alunoRepository ?? throw new ArgumentNullException(nameof(alunoRepository));
        }

        CultureInfo br = new CultureInfo("pt-BR");

        [HttpGet]
        public async Task<IActionResult> ObterTodos()
        {
            var alunos = await _alunoRepository.ObterTodos();

            return Ok(alunos);
        }
        [HttpGet("{id}")]
        public async Task<IActionResult> ObterPorId(int id)
        {
            var aluno = await _alunoRepository.ObterPorId(id);
            
            if (aluno == null)
            {
                Log.Error("Not found");
                return NotFound(id);
            }
            return Ok(aluno);
        }
        
        [HttpPost]
        public async Task<IActionResult> Adicionar([FromBody] AlunoDTO aluno_recebido)
        {
            Aluno aluno = new Aluno();
            {
                aluno.NOME = aluno_recebido.Nome;
                aluno.MATRICULA = aluno_recebido.Matricula;
                aluno.CURSO = aluno_recebido.Curso;

                DateTime nascimento_aluno = Convert.ToDateTime(aluno_recebido.Nascimento, br);
                aluno.DATA_NASCIMENTO = nascimento_aluno;

                DateTime ingresso_aluno = Convert.ToDateTime(aluno_recebido.Ingresso, br);
                aluno.DATA_INGRESSO = ingresso_aluno;
            }
            
            await _alunoRepository.Adicionar(aluno);

            return Ok(aluno);
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> Atualizar(int id, [FromBody] AlunoDTO aluno_recebido)
        {
            Aluno aluno = new Aluno();
            {
                aluno.NOME = aluno_recebido.Nome;
                aluno.MATRICULA = aluno_recebido.Matricula;
                aluno.CURSO = aluno_recebido.Curso;

                DateTime nascimento_aluno = Convert.ToDateTime(aluno_recebido.Nascimento, br);
                aluno.DATA_NASCIMENTO = nascimento_aluno;

                DateTime ingresso_aluno = Convert.ToDateTime(aluno_recebido.Ingresso, br);
                aluno.DATA_INGRESSO = ingresso_aluno;

                aluno.ID = id;
            }
            await _alunoRepository.Atualizar(aluno);
            
            return Ok(aluno);
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> Deletar(int id)
        {
            await _alunoRepository.Deletar(id);
            return Ok();
        }

    }
}
