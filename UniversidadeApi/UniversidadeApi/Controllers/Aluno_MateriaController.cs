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

        /*public Aluno_Materia DTOParaAluno_Materia (Aluno_MateriaDTO dTO)
        {
            var aluno_materia = new Aluno_Materia();

            aluno_materia.MATERIA_ID = dTO.Materia_id;
            aluno_materia.ALUNO_ID = dTO.Aluno_id;

            return aluno_materia;
        }*/

        [HttpGet]
        public IActionResult GetAll()
        {
            var alunos_materias = _aluno_MateriaRepository.GetAll();

            return Ok(alunos_materias);
        }

        [HttpGet("descricao")]
        public IActionResult GetAllDesc()
        {
            var aluno_materia_res = _aluno_MateriaRepository.GetAllDesc();

            return Ok(aluno_materia_res);
        }

        [HttpPost]
        public IActionResult Add([FromBody] Aluno_MateriaDTO aluno_materia_recebido)
        {
            var aluno_materia = new Aluno_Materia();
            {
                aluno_materia.MATERIA_ID = aluno_materia_recebido.Materia_id;
                aluno_materia.ALUNO_ID = aluno_materia_recebido.Aluno_id;
            }

            _aluno_MateriaRepository.Add(aluno_materia);

            return Ok(aluno_materia);
        }

        [HttpPut("{id}")]
        public IActionResult Update(int id, [FromBody] Aluno_MateriaDTO aluno_materia_recebido)
        {
            var aluno_materia = new Aluno_Materia();
            {
                aluno_materia.ID = id;
                aluno_materia.MATERIA_ID = aluno_materia_recebido.Materia_id;
                aluno_materia.ALUNO_ID = aluno_materia_recebido.Aluno_id;
            }

            _aluno_MateriaRepository.Update(aluno_materia);

            return Ok(aluno_materia);
        }

        [HttpDelete("{id}")]
        public IActionResult Delete(int id)
        {
            var aluno_materia = _aluno_MateriaRepository.Get(id);

            _aluno_MateriaRepository.Delete(aluno_materia);
            return Ok(aluno_materia);
        }
    }
}
