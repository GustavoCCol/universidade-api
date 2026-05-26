using Microsoft.EntityFrameworkCore;
using UniversidadeApi.Infrastucture.Interfaces;
using UniversidadeApi.Models;
using Serilog;
using System.Linq.Expressions;
using Microsoft.AspNetCore.Http.HttpResults;
using UniversidadeApi.Infrastucture.Context;

namespace UniversidadeApi.Application.Services
{
    public class AlunoService : BaseRepository<Aluno>, IAlunoRepository
    {
        public AlunoService(ConnectionContext context) : base(context)
        {
        }
    }
}
