namespace ariefmahendra.Repositories;

public class Persistence : IPersistence
{
    private readonly ApplicationDbContext _applicationDbContext;

    public Persistence(ApplicationDbContext applicationDbContext)
    {
        _applicationDbContext = applicationDbContext;
    }

    public async Task SaveChangesAsync()
    {
        await _applicationDbContext.SaveChangesAsync();
    }

    public async Task BeginTransaction()
    {
        await _applicationDbContext.Database.BeginTransactionAsync();
    }

    public async Task Commit()
    {
        await _applicationDbContext.Database.CommitTransactionAsync();
    }

    public async Task Rollback()
    {
        await _applicationDbContext.Database.RollbackTransactionAsync();
    }
}