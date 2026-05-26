using UniversidadeApi.Infrastucture.Context;
using UniversidadeApi.Infrastucture.Interfaces;
using UniversidadeApi.Models;

namespace UniversidadeApi.Application.Services
{
    public class MateriaService : BaseRepository<Materia>, IMateriaRepository
    {
        public MateriaService(ConnectionContext context) : base(context) { }
    }
}
