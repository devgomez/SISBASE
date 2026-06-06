namespace SISBase.Domain.Interfaces
{
    public interface ICrudRepository<TEntity>
        where TEntity : class
    {
        Task<List<TEntity>> GetAllAsync();

        Task<TEntity?> GetByIdAsync(int id);

        Task AddAsync(TEntity entity);

        Task UpdateAsync(TEntity entity);

        Task DeleteAsync(int id);
    }
}