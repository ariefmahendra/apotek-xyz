using ariefmahendra.Dtos;
using ariefmahendra.Entities;
using ariefmahendra.Repositories;
using ariefmahendra.Utils.CustomException;
using ariefmahendra.Utils.MapObject;

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

    public async Task<ProductResponse> Create(Product payload)
    {
        var product = await _repository.SaveAsync(payload);
        await _persistence.SaveChangesAsync();
        return Map.MapProductResponse(product);
    }

    public async Task<ProductResponse> GetById(string? id)
    {
        var product = await _repository.FindByIdAsync(Guid.Parse(id));
        if (product == null)
        {
            throw new NotFoundException("Product Not Found");
        }

        return Map.MapProductResponse(product);
    }

    public async Task<List<ProductResponse>> GetAll()
    {
        var products = await _repository.FindAllAsync();
        return products.Select(Map.MapProductResponse).ToList();
    }

    public async Task<ProductResponse> Update(Product payload)
    {
        var product = _repository.Update(payload);
        await _persistence.SaveChangesAsync();
        return Map.MapProductResponse(product);
    }

    public async Task DeleteById(string? id)
    {
        var product = await _repository.FindByIdAsync(Guid.Parse(id));
        if (product == null)
        {
            throw new NotFoundException("Product Not Found");
        }
        _repository.Delete(product);
        await _persistence.SaveChangesAsync();
    }
}