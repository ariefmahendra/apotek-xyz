using System.Linq.Expressions;

namespace ariefmahendra.Repositories;

public interface IRepository<TEntity>
{
    Task<TEntity> SaveAsync(TEntity entity);
    TEntity Attach (TEntity entity);
    Task<TEntity?> FindByIdAsync(Guid id);
    Task<TEntity?> FindAsync(Expression<Func<TEntity, bool>> criteria);
    Task<List<TEntity>> FindAllAsync();
    TEntity Update(TEntity entity);
    void Delete(TEntity entity);
}