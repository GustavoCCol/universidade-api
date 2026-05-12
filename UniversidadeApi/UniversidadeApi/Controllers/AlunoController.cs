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
    public class AlunoController : ControllerBase
    {
        private readonly IAlunoRepository _alunoRepository;

        public AlunoController(IAlunoRepository alunoRepository)
        {
            _alunoRepository = alunoRepository ?? throw new ArgumentNullException(nameof(alunoRepository));
        }

        CultureInfo br = new CultureInfo("pt-BR");

        [HttpGet]
        public IActionResult ObterTodos()
        {
            var alunos = _alunoRepository.ObterTodos();

            return Ok(alunos);
        }
        [HttpGet("{id}")]
        public IActionResult Obter(int id)
        {
            var aluno = _alunoRepository.Obter(id);
            
            if (aluno == null)
            {
                Log.Error("Not found");
                return NotFound(id);
            }
            return Ok(aluno);
        }
        
        [HttpPost]
        public IActionResult Adicionar([FromBody] AlunoDTO aluno_recebido)
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
            
            _alunoRepository.Adicionar(aluno);

            return Ok();
        }

        [HttpPut("{id}")]
        public IActionResult Atualizar(int id, [FromBody] AlunoDTO aluno_recebido)
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
            _alunoRepository.Atualizar(aluno);
            
            return Ok(aluno);
        }

        [HttpDelete("{id}")]

        public void Deletar(int id)
        {
            var aluno_deletado = _alunoRepository.Obter(id);
            
            _alunoRepository.Deletar(aluno_deletado);
        }

    }
}
