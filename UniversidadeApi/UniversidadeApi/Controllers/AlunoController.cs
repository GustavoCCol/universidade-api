using Microsoft.AspNetCore.Mvc;
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
        public IActionResult GetAll()
        {
            var alunos = _alunoRepository.GetAll();

            return Ok(alunos);
        }
        [HttpGet("{id}")]
        public IActionResult Get(int id)
        {
            var aluno = _alunoRepository.Get(id);
            
            return Ok(aluno);
        }
        
        [HttpPost]
        public IActionResult Add([FromBody] AlunoDTO aluno_recebido)
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
            
            _alunoRepository.Add(aluno);

            return Ok();
        }

        [HttpPut("{id}")]
        public IActionResult Update(int id, [FromBody] AlunoDTO aluno_recebido)
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
            _alunoRepository.Update(aluno);
            
            return Ok(aluno);
        }

        [HttpDelete("{id}")]

        public void Delete(int id)
        {
            var aluno_deletado = _alunoRepository.Get(id);
            
            _alunoRepository.Delete(aluno_deletado);
        }

    }
}
