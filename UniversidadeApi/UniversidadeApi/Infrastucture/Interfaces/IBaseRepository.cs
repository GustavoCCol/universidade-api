using System.Data.Entity;
using System.Linq.Expressions;
using UniversidadeApi.Models;

namespace UniversidadeApi.Infrastucture.Interfaces
{
    public interface IBaseRepository<T> where T : Base
    {
        Task<T> ObterPorId(int id);
        Task<List<T>> ObterTodos();
        Task Adicionar(T entity);
        Task Atualizar(T entity);
        Task Deletar(int id);
        Task<T> ObterPorCondicaoAsync(Expression<Func<T, bool>> predicate, params Expression<Func<T, object>>[] includes);
        Task<IQueryable<T>> ObterTodosPorCondicaoAsync(Expression<Func<T, bool>> predicate, params Expression<Func<T, object>>[] includes);
    }
}
