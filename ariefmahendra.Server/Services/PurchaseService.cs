using ariefmahendra.Dtos;
using ariefmahendra.Entities;
using ariefmahendra.Repositories;
using ariefmahendra.Utils.CustomException;
using ariefmahendra.Utils.MapObject;

namespace ariefmahendra.Services;

public class PurchaseService: IPurchaseService
{

    private readonly IPersistence _persistence;
    private readonly IRepository<Purchase> _purchaseRepository;
    private readonly IRepository<PurchaseDetail> _PurchaseDetailRepository;
    private readonly IRepository<Product> _ProductRepository;

    public PurchaseService(IPersistence persistence, IRepository<Purchase> purchaseRepository, IRepository<PurchaseDetail> purchaseDetailRepository, IRepository<Product> productRepository)
    {
        _persistence = persistence;
        _purchaseRepository = purchaseRepository;
        _PurchaseDetailRepository = purchaseDetailRepository;
        _ProductRepository = productRepository;
    }

    public async Task<PurchaseResponse> CreateNewTransaction(Purchase payload)
    {

        await _persistence.BeginTransaction();
        try
        {
            long totalTransaction = 0;

            if (payload.PurchaseDetails == null)
            {
                throw new BadRequestException("must be set instance of purchase detail");
            }

            var productList = new List<Product>();
            foreach (var purchaseDetail in payload.PurchaseDetails)
            {
                // get product is present / else throw exception not found
                var productById = await _ProductRepository.FindByIdAsync(purchaseDetail.ProductId);

                if (productById == null)
                {
                    throw new NotFoundException("product not found");
                }
                
                // update stock product
                if (purchaseDetail.Quantity > productById.Stock)
                {
                    throw new BadRequestException("quantity not valid");
                }
                
                var productUpdated = _ProductRepository.Update(productById);
                await _persistence.SaveChangesAsync();

                var product = productUpdated ?? throw new Exception("failed updated product");
                
                // add product to collection
                productList.Add(product);
                    
                totalTransaction += productById.ProductPrice * purchaseDetail.Quantity;
            }

            // save purchase
            payload.Total = totalTransaction;
            var purchase = await _purchaseRepository.SaveAsync(payload);
            await _persistence.SaveChangesAsync();

            var i = 0;
            var j = 0;
            var purchaseDetailResponse = new List<PurchaseDetailResponse>();
            
            while (i < productList.Count() -1 && j < payload.PurchaseDetails.Count() -1)
            {
                var purchaseDetailEntity = await _PurchaseDetailRepository.SaveAsync(new PurchaseDetail()
                {
                    PurchaseId = purchase.Id,
                    ProductId = productList[i].Id,
                    Quantity = payload.PurchaseDetails[j].Quantity
                });
                await _persistence.SaveChangesAsync();
                i++;
                j++;

                purchaseDetailResponse.Add(new PurchaseDetailResponse()
                {
                    Id = purchaseDetailEntity.Id.ToString(),
                    Product = productList[i],
                    Quantity = payload.PurchaseDetails[j].Quantity
                });
            }

            
            // commit transaction
            await _persistence.Commit();
            
            return new PurchaseResponse()
            {
                Id = purchase.Id.ToString(),
                TransactionDate = purchase.TransactionDate,
                Total = purchase.Total,
                NoInvoice = purchase.NoInvoice,
                PurchaseDetailResponses = purchaseDetailResponse,
            };
        }
        catch (Exception e)
        {
            await _persistence.Rollback();
            throw new Exception(e.Message);
        }
    }
}