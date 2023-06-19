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

        public async Task<T> getById(int id)
        {
            return await table.FindAsync(id);
        }
        public async Task<List<T>> getAll(int page)
        {
            return await table.Skip(page*10).Take(10).ToListAsync();
        }
        public async Task<T> updateById(int id, T entity)
        {
            var _entity = table.Find(id);
            _entity = entity;
            await _dataContext.SaveChangesAsync();
            return _entity;
        }
        public async Task<T> deleteById(int id)
        {

        }
        public async Task<T> add(T entity)
        {

        }
        public async Task<T> deleteAll()
        {

        }
    }
}
