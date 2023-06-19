using API.Interfaces;
using DataContext;
using Microsoft.EntityFrameworkCore;


namespace API.Repositories
{
    public class CrudRepository<T> : ICrudRepository<T> where T : class
    {

        private Datacontext _dataContext = null;
        private DbSet<T> table = null;

        public CrudRepository()
        {
            _dataContext = new Datacontext();
            table = _dataContext.Set<T>();
        }

        public Task<T> getById(int id)
        {

        }
        public Task<T> getAll(int page)
        {

        }
        public Task<T> updateById(int id, T entity)
        {

        }
        public Task<T> deleteById(int id)
        {

        }
        public Task<T> add(T entity)
        {

        }
        public Task<T> deleteAll()
        {

        }
    }
}
