namespace API.Repositories
{
    public interface ICrudRepository<T>
    {
        public Task<T> deleteById(int id);

        public Task<T> add(T entity);

        public Task<T> deleteAll();
    }
}
