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
        public IActionResult ObterTodas()
        {
            var notas = _notaRepository.ObterTodas();
            return Ok(notas);
        }

        [HttpGet("descricao")]
        public IActionResult ObterTodasDesc()
        {
            var notas_res = _notaRepository.ObterTodasDesc();
            return Ok(notas_res);
        }

        [HttpGet("{id}")]
        public IActionResult Obter(int id)
        {
            var nota = _notaRepository.Obter(id);
            return Ok(nota);
        }

        [HttpGet("bimestre/{bimestre}/{aluno_id}")]
        public IActionResult ObterPorBimestre(int bimestre, int aluno_id)
        {
            var notas = _notaRepository.ObterPorBimestre(bimestre, aluno_id);
            if (notas == null) return NotFound("Aluno ou bimestre inválido");
            return Ok(notas);
        }
        [HttpGet("materia/{materia_id}/{aluno_id}")]
        public IActionResult ObterPorMateria(int materia_id, int aluno_id)
        {
            var notas = _notaRepository.ObterPorMateria(materia_id, aluno_id);
            if (notas == null) return NotFound("Aluno ou materia inválido(a)");
            return Ok(notas);
        }
        [HttpPost]
        public IActionResult Adicionar([FromBody] NotaDTO nota_recebida)
        {
            var nota = new Nota();
            {
                nota.MATERIA_ID = nota_recebida.Materia_id;
                nota.ALUNO_ID = nota_recebida.Aluno_id;
                nota.BIMESTRE = nota_recebida.Bimestre;
                nota.NOTA = nota_recebida.Nota;
            }

            bool add = _notaRepository.Adicionar(nota);
            if (!add) return BadRequest("Não foi possível adicionar nota, verificar valores colocados");
            return Ok(nota);
        }

        [HttpPut("{id}")]
        public IActionResult Atualizar(int id, [FromBody] NotaDTO nota_recebida)
        {
            var nota = new Nota();
            {
                nota.ID = id;
                nota.MATERIA_ID = nota_recebida.Materia_id;
                nota.ALUNO_ID = nota_recebida.Aluno_id;
                nota.BIMESTRE = nota_recebida.Bimestre;
                nota.NOTA = nota_recebida.Nota;
            }

            _notaRepository.Atualizar(nota);

            return Ok(nota);
        }
        [HttpDelete("{id}")]
        public IActionResult Deletar(int id)
        {
            var nota = _notaRepository.Obter(id);
            _notaRepository.Deletar(nota);
            return Ok(nota);
        }
    }
}
