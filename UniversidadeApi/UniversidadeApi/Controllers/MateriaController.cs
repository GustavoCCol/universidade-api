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
        public IActionResult ObterTodas()
        {
            var materias = _materiaRepository.ObterTodas();

            return Ok(materias);
        }

        [HttpGet("{id}")]
        public IActionResult Obter(int id)
        {
            var materia = _materiaRepository.Obter(id);

            return Ok(materia);
        }

        [HttpPost]
        public IActionResult Adicionar([FromBody] MateriaDTO materia_recebida)
        {
            var materia = new Materia();
            {
                materia.ID = materia_recebida.Id;
                materia.PROFESSOR = materia_recebida.Professor;
                materia.NOME = materia_recebida.Nome;
            }

            _materiaRepository.Adicionar(materia);

            return Ok(materia);
        }

        [HttpPut("{id}")]
        public IActionResult Atualizar(int id, [FromBody] MateriaDTO materia_recebida)
        {
            var materia = new Materia();
            {
                materia.ID = id;
                materia.PROFESSOR = materia_recebida.Professor;
                materia.NOME = materia_recebida.Nome;
            }

            _materiaRepository.Atualizar(materia);
           
            return Ok(materia);
        }

        [HttpDelete("{id}")]
        public IActionResult Deletar(int id)
        {
            var materia_deletada = _materiaRepository.Obter(id);
            
            _materiaRepository.Deletar(materia_deletada);
            
            return Ok(materia_deletada);
        }
    }
}
