using Microsoft.AspNetCore.Mvc;
using UniversidadeApi.DTOs;
using UniversidadeApi.Infrastucture.Context;
using UniversidadeApi.Infrastucture.Interfaces;
using UniversidadeApi.Models;

namespace UniversidadeApi.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class Aluno_MateriaController : Controller
    {
        private readonly IAluno_MateriaRepository _aluno_MateriaRepository;
        public Aluno_MateriaController(IAluno_MateriaRepository aluno_MateriaRepository)
        {
            _aluno_MateriaRepository = aluno_MateriaRepository ?? throw new ArgumentNullException(nameof(aluno_MateriaRepository));
        }

        [HttpGet]
        public async Task<IActionResult> ObterTodos()
        {
            var alunos_materias = await _aluno_MateriaRepository.ObterTodos();

            return Ok(alunos_materias);
        }

        [HttpGet("descricao")]
        public async Task<IActionResult> ObterTodosDesc()
        {
            var aluno_materia_res = await _aluno_MateriaRepository.ObterTodosDesc();

            return Ok(aluno_materia_res);
        }

        [HttpGet("cadastros/aluno/{id}")]
        public async Task<IActionResult> ObterCadastrosAlunoPorId(int id)
        {
            var cadastros = await _aluno_MateriaRepository.ObterCadastrosAlunoPorId(id);
            if (cadastros == null) return NotFound($"Nenhum cadastro encontrado para o aluno {id}");
            return Ok(cadastros);
        }

        [HttpGet("cadastros/materia/{id}")]
        public async Task<IActionResult> ObterCadastrosMateriaPorId (int id)
        {
            var cadastros = await _aluno_MateriaRepository.ObterCadastrosMateriaPorId(id);
            if (cadastros == null) return NotFound($"Nenhum cadastro encontrado para a matéria {id}");
            return Ok(cadastros);
        }
        [HttpPost]
        public async Task<IActionResult> Adicionar([FromBody] Aluno_MateriaDTO aluno_materia_recebido)
        {
            var aluno_materia = new Aluno_Materia();
            {
                aluno_materia.MATERIA_ID = aluno_materia_recebido.Materia_id;
                aluno_materia.ALUNO_ID = aluno_materia_recebido.Aluno_id;
            }

            await _aluno_MateriaRepository.Adicionar(aluno_materia);

            return Ok(aluno_materia);
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> Atualizar(int id, [FromBody] Aluno_MateriaDTO aluno_materia_recebido)
        {
            var aluno_materia = new Aluno_Materia();
            {
                aluno_materia.ID = id;
                aluno_materia.MATERIA_ID = aluno_materia_recebido.Materia_id;
                aluno_materia.ALUNO_ID = aluno_materia_recebido.Aluno_id;
            }

            await _aluno_MateriaRepository.Atualizar(aluno_materia);

            return Ok(aluno_materia);
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> Deletar(int id)
        {
            await _aluno_MateriaRepository.Deletar(id);
            return Ok();
        }
    }
}
