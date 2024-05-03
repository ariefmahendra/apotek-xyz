using ariefmahendra.Entities;
using ariefmahendra.Repositories;
using ariefmahendra.Utils.CustomException;

namespace ariefmahendra.Services;

public class ProductService : IProductService
{
    private readonly IPersistence _persistence;
    private readonly IRepository<Product> _repository;

    public ProductService(IPersistence persistence, IRepository<Product> repository)
    {
        _persistence = persistence;
        _repository = repository;
    }

    public async Task<Product> Create(Product payload)
    {
        var product = await _repository.SaveAsync(payload);
        await _persistence.SaveChangesAsync();
        return product;
    }

    public async Task<Product> GetById(string? id)
    {
        var product = await _repository.FindByIdAsync(Guid.Parse(id));
        if (product == null)
        {
            throw new NotFoundException("Product Not Found");
        }

        return product;
    }

    public async Task<List<Product>> GetAll()
    {
        return await _repository.FindAllAsync();
    }

    public async Task<Product> Update(Product payload)
    {
        var product = _repository.Update(payload);
        await _persistence.SaveChangesAsync();
        return product;
    }

    public async Task DeleteById(string? id)
    {
        var byId = await GetById(id);
        _repository.Delete(byId);
        await _persistence.SaveChangesAsync();
    }
}