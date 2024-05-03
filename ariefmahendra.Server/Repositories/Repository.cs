using Microsoft.EntityFrameworkCore;
using Microsoft.VisualBasic;

namespace ariefmahendra.Repositories;

public class Repository<TEntity> : IRepository<TEntity> where TEntity : class
{

    private readonly ApplicationDbContext _applicationDbContext;

    public Repository(ApplicationDbContext applicationDbContext)
    {
        _applicationDbContext = applicationDbContext;
    }

    public async Task<TEntity> SaveAsync(TEntity entity)
    {
        var entry = await _applicationDbContext.Set<TEntity>().AddAsync(entity);
        return entry.Entity;
    }

    public TEntity Attach(TEntity entity)
    {
        var entry = _applicationDbContext.Set<TEntity>().Attach(entity);
        return entry.Entity;
    }

    public async Task<TEntity?> FindByIdAsync(Guid id)
    {
        return await _applicationDbContext.Set<TEntity>().FindAsync(id);
    }

    public async Task<List<TEntity>> FindAllAsync()
    {
        return await _applicationDbContext.Set<TEntity>().ToListAsync();
    }

    public TEntity Update(TEntity entity)
    {
        Attach(entity);
        var entry = _applicationDbContext.Set<TEntity>().Update(entity);
        return entry.Entity;
    }

    public void Delete(TEntity entity)
    {
        _applicationDbContext.Set<TEntity>().Remove(entity);
    }
}

