namespace API.Interfaces
{
    public interface ICrudRepository
    {
        public Task<T> deleteById(int id);

        public Task<T> add(T entity);

        public Task<T> deleteAll();
    }
}
