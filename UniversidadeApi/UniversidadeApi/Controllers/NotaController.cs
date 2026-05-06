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
        public IActionResult GetAll()
        {
            var notas = _notaRepository.GetAll();
            return Ok(notas);
        }

        [HttpGet("descricao")]
        public IActionResult GetAllDesc()
        {
            var notas_res = _notaRepository.GetAllDesc();
            return Ok(notas_res);
        }

        [HttpGet("{id}")]
        public IActionResult Get(int id)
        {
            var nota = _notaRepository.Get(id);
            return Ok(nota);
        }

        [HttpPost]
        public IActionResult Add([FromBody] NotaDTO nota_recebida)
        {
            var nota = new Nota();
            {
                nota.MATERIA_ID = nota_recebida.Materia_id;
                nota.ALUNO_ID = nota_recebida.Aluno_id;
                nota.BIMESTRE = nota_recebida.Bimestre;
                nota.NOTA = nota_recebida.Nota;
            }

            _notaRepository.Add(nota);
            
            return Ok(nota);
        }

        [HttpPut("{id}")]
        public IActionResult Update(int id, [FromBody] NotaDTO nota_recebida)
        {
            var nota = new Nota();
            {
                nota.ID = id;
                nota.MATERIA_ID = nota_recebida.Materia_id;
                nota.ALUNO_ID = nota_recebida.Aluno_id;
                nota.BIMESTRE = nota_recebida.Bimestre;
                nota.NOTA = nota_recebida.Nota;
            }

            _notaRepository.Update(nota);

            return Ok(nota);
        }
        [HttpDelete("{id}")]
        public IActionResult Delete(int id)
        {
            var nota = _notaRepository.Get(id);
            _notaRepository.Delete(nota);
            return Ok(nota);
        }
    }
}
