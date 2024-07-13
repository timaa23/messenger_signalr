using back_messenger_signalr.Entities;

namespace back_messenger_signalr.Repositories.Interfaces
{
    public interface IGenericRepository<TEntity, T> where TEntity : class, IEntity<T>
    {
        IQueryable<TEntity> GetAll();

        IQueryable<TEntity> GetById(T id);

        Task InsertAsync(TEntity entity);

        Task UpdateAsync(TEntity entity);

        Task DeleteAsync(TEntity entity);
    }
}
