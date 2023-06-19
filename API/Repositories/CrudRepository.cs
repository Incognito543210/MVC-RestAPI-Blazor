using API.Interfaces;

namespace API.Repositories
{
    public class CrudRepository<T> : ICrudRepository<T> where T : class
    {
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
