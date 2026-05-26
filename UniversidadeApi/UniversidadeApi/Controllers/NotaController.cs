using Microsoft.AspNetCore.Mvc;
using UniversidadeApi.DTOs;
using UniversidadeApi.Infrastucture.Context;
using UniversidadeApi.Infrastucture.Interfaces;
using UniversidadeApi.Models;

namespace UniversidadeApi.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class NotaController : ControllerBase
    {
        private readonly INotaRepository _notaRepository;

        public NotaController(INotaRepository notaRepository)
        {
            _notaRepository = notaRepository ?? throw new ArgumentNullException(nameof(notaRepository));
        }
        
        [HttpGet]
        public async Task<IActionResult> ObterTodas()
        {
            var notas = await _notaRepository.ObterTodos();
            return Ok(notas);
        }

        [HttpGet("descricao")]
        public async Task<IActionResult> ObterTodasDesc()
        {
            var notas_res = await _notaRepository.ObterTodasDesc();
            return Ok(notas_res);
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> Obter(int id)
        {
            var nota = await _notaRepository.ObterPorId(id);
            return Ok(nota);
        }

        [HttpGet("bimestre/{bimestre}/{aluno_id}")]
        public async Task<IActionResult> ObterPorBimestre(int bimestre, int aluno_id)
        {
            var notas = await _notaRepository.ObterPorBimestre(bimestre, aluno_id);
            if (notas == null) return NotFound("Aluno ou bimestre inválido");
            return Ok(notas);
        }
        [HttpGet("materia/{materia_id}/{aluno_id}")]
        public async Task<IActionResult> ObterPorMateria(int materia_id, int aluno_id)
        {
            var notas = await _notaRepository.ObterPorMateria(materia_id, aluno_id);
            if (notas == null) return NotFound("Aluno ou materia inválido(a)");
            return Ok(notas);
        }
        [HttpPost]
        public async Task<IActionResult> Adicionar([FromBody] NotaDTO nota_recebida)
        {
            var nota = new Nota();
            {
                nota.MATERIA_ID = nota_recebida.Materia_id;
                nota.ALUNO_ID = nota_recebida.Aluno_id;
                nota.BIMESTRE = nota_recebida.Bimestre;
                nota.NOTA = nota_recebida.Nota;
            }

            bool add = await _notaRepository.AdicionarNota(nota);
            if (!add) return BadRequest("Não foi possível adicionar nota, verificar valores colocados");
            return Ok(nota);
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> Atualizar(int id, [FromBody] NotaDTO nota_recebida)
        {
            var nota = new Nota();
            {
                nota.ID = id;
                nota.MATERIA_ID = nota_recebida.Materia_id;
                nota.ALUNO_ID = nota_recebida.Aluno_id;
                nota.BIMESTRE = nota_recebida.Bimestre;
                nota.NOTA = nota_recebida.Nota;
            }

            await _notaRepository.Atualizar(nota);

            return Ok(nota);
        }
        [HttpDelete("{id}")]
        public async Task<IActionResult> Deletar(int id)
        {
            await _notaRepository.Deletar(id);
            return Ok();
        }
    }
}
