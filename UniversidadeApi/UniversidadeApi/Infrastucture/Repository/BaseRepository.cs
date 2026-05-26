using Microsoft.EntityFrameworkCore;
using System.Linq.Expressions;
using UniversidadeApi.Infrastucture.Interfaces;
using UniversidadeApi.Models;


namespace UniversidadeApi.Infrastucture.Context
{
    public class BaseRepository<T> : IBaseRepository<T> where T: Base
    {
        private readonly ConnectionContext _context;
        private readonly DbSet<T> _entities;

        public BaseRepository(ConnectionContext context)
        {
            _context = context ?? throw new ArgumentNullException(nameof(context));
            _entities = context.Set<T>();
        }

        public async Task<T> ObterPorId(int id)
        {
                return await _entities.FirstOrDefaultAsync(s => s.ID == id);
        }

        public async Task<List<T>> ObterTodos()
        {
            return await _entities.ToListAsync();
        }

        public async Task Adicionar(T entity)
        {
            await _entities.AddAsync(entity);
            await _context.SaveChangesAsync();
        }
        
        public async Task Atualizar(T entity)
        {
            _entities.Update(entity);
            await _context.SaveChangesAsync();
        }

        public async Task Deletar(int id)
        {
            _entities.Remove(ObterPorId(id).Result);
            await _context.SaveChangesAsync();
        }

        public async Task<T> ObterPorCondicaoAsync(Expression<Func<T, bool>> predicate, params Expression<Func<T, object>>[] includes)
        {
            IQueryable<T> query = _entities;

            if (predicate != null)
                query = query.Where(predicate);

            foreach (var include in includes)
            {
                query = query.Include(include);
            }

            return await query.SingleOrDefaultAsync();
        }
        public async Task<IQueryable<T>> ObterTodosPorCondicaoAsync(Expression<Func<T, bool>> predicate, params Expression<Func<T, object>>[] includes)
        {
            IQueryable<T> query = _entities;
            await Task.Run(() =>
            {
                if (predicate != null)
                    query = query.Where(predicate);

                foreach (var include in includes)
                {
                    query = query.Include(include);
                }
            });

            return query;
        }

    }
}
