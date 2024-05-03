namespace ariefmahendra.Repositories;

public interface IPersistence
{
    Task SaveChangesAsync();
    Task BeginTransaction();
    Task Commit();
    Task Rollback();
}