namespace Infraestructure.Repositories.Interfaces
{
    public interface IRepository<T, I> where T : class
    {
        Task<IEnumerable<T?>> Get();
        Task<T?> GetById(I id);
        Task<T> Create(T model);
        Task<T> Update(T model);
        Task Delete(T model);
    }
}