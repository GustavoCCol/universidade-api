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
        public IActionResult GetAll()
        {
            var materias = _materiaRepository.GetAll();

            return Ok(materias);
        }

        [HttpGet("{id}")]
        public IActionResult Get(int id)
        {
            var materia = _materiaRepository.Get(id);

            return Ok(materia);
        }

        [HttpPost]
        public IActionResult Add([FromBody] MateriaDTO materia_recebida)
        {
            var materia = new Materia();
            {
                materia.ID = materia_recebida.Id;
                materia.PROFESSOR = materia_recebida.Professor;
                materia.NOME = materia_recebida.Nome;
            }

            _materiaRepository.Add(materia);

            return Ok(materia);
        }

        [HttpPut("{id}")]
        public IActionResult Update(int id, [FromBody] MateriaDTO materia_recebida)
        {
            var materia = new Materia();
            {
                materia.ID = id;
                materia.PROFESSOR = materia_recebida.Professor;
                materia.NOME = materia_recebida.Nome;
            }

            _materiaRepository.Update(materia);
           
            return Ok(materia);
        }

        [HttpDelete("{id}")]
        public IActionResult Delete(int id)
        {
            var materia_deletada = _materiaRepository.Get(id);
            
            _materiaRepository.Delete(materia_deletada);
            
            return Ok(materia_deletada);
        }
    }
}
