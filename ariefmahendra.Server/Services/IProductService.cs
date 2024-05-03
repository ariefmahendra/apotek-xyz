using ariefmahendra.Dtos;
using ariefmahendra.Entities;

namespace ariefmahendra.Services;

public interface IProductService
{
    Task<ProductResponse> Create(Product payload);
    Task<ProductResponse> GetById(string? id);
    Task<List<ProductResponse>> GetAll();
    Task<ProductResponse> Update(Product payload);
    Task DeleteById(string? id);
}