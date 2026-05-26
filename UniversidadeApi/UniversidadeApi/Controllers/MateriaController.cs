using Microsoft.AspNetCore.Mvc;
using UniversidadeApi.Infrastucture.Interfaces;
using UniversidadeApi.DTOs;
using UniversidadeApi.Models;

namespace UniversidadeApi.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class MateriaController : ControllerBase
    {
        private readonly IMateriaRepository _materiaRepository;

        public MateriaController(IMateriaRepository materiaRepository)
        {
            _materiaRepository = materiaRepository ?? throw new ArgumentNullException(nameof(materiaRepository));
        }

        [HttpGet]
        public async Task<IActionResult> ObterTodas()
        {
            var materias = await _materiaRepository.ObterTodos();

            return Ok(materias);
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> Obter(int id)
        {
            var materia = await _materiaRepository.ObterPorId(id);

            return Ok(materia);
        }

        [HttpPost]
        public async Task<IActionResult> Adicionar([FromBody] MateriaDTO materia_recebida)
        {
            var materia = new Materia();
            {
                materia.ID = materia_recebida.Id;
                materia.PROFESSOR = materia_recebida.Professor;
                materia.NOME = materia_recebida.Nome;
            }

            await _materiaRepository.Adicionar(materia);

            return Ok(materia);
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> Atualizar(int id, [FromBody] MateriaDTO materia_recebida)
        {
            var materia = new Materia();
            {
                materia.ID = id;
                materia.PROFESSOR = materia_recebida.Professor;
                materia.NOME = materia_recebida.Nome;
            }

            await _materiaRepository.Atualizar(materia);
           
            return Ok(materia);
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> Deletar(int id)
        {   
            await _materiaRepository.Deletar(id);
            return Ok();
        }
    }
}
