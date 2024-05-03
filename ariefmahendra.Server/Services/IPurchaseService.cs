using ariefmahendra.Dtos;
using ariefmahendra.Entities;

namespace ariefmahendra.Services;

public interface IPurchaseService
{
    Task<PurchaseResponse> CreateNewTransaction(Purchase payload);
}