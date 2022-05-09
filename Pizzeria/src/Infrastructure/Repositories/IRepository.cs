using System.Linq.Expressions;

namespace Pizzeria.Infrastructure.Repositories
{
    public interface IRepository<T> 
        where T : class
    {
        public IQueryable<T> GetAll();

        public Task<T> GetByIdAsync(int id);

        public Task<T> AddAsync(T entity);

        public Task RemoveAsync(int id);

        public Task UpdateAsync(T entity);
    }
}
