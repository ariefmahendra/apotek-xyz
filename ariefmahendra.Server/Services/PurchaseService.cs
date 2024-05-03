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
    private readonly IProductService _productService;
    
    public PurchaseService(IPersistence persistence, IRepository<Purchase> purchaseRepository, IProductService productService)
    {
        _persistence = persistence;
        _purchaseRepository = purchaseRepository;
        _productService = productService;
    }

    public async Task<PurchaseResponse> CreateNewTransaction(Purchase payload)
    {

        await _persistence.BeginTransaction();
        try
        {
            // save purchase
            var purchase = await _purchaseRepository.SaveAsync(payload);
            await _persistence.SaveChangesAsync();

            // store total transaction and purchase detail response
            var purchaseDetails = new List<PurchaseDetailResponse>();
            long totalTransaction = 0;
            
            if (payload.PurchaseDetails != null)
                foreach (var purchaseDetail in payload.PurchaseDetails)
                {
                    // set purchase detail to reference purchase id 
                    purchaseDetail.PurchaseId = purchase.Id;
                    
                    // get product is present / else throw exception not found
                    var productById = await _productService.GetById(purchaseDetail.ProductId.ToString());

                    var productEntity = Map.MapProductEntity(productById);

                    // update stock product id
                    productById.Stock -= purchaseDetail.Quantity;
                    var productUpdated = await _productService.Update(productEntity);
                    await _persistence.SaveChangesAsync();
                        
                    // store purchase detail to purchase detail response
                    purchaseDetails.Add(new PurchaseDetailResponse()
                    {
                        Id = purchaseDetail.Id.ToString(),
                        Product = Map.MapProductEntity(productUpdated),
                        Quantity = purchaseDetail.Quantity
                    });
                    
                    if (productById.Stock <= 0)
                    {
                        throw new Exception("stock empty");
                    }
                    totalTransaction += productById.ProductPrice * purchaseDetail.Quantity;
                }

            // update total transaction on purchase
            purchase.Total = totalTransaction;
            _purchaseRepository.Update(purchase);
            await _persistence.SaveChangesAsync();
            
            // commit transaction
            await _persistence.Commit();

            return new PurchaseResponse()
            {
                Id = purchase.Id.ToString(),
                TransactionDate = purchase.TransactionDate,
                Total = purchase.Total,
                NoInvoice = purchase.NoInvoice,
                PurchaseDetailResponses = purchaseDetails,
            };
        }
        catch (Exception e)
        {
            await _persistence.Rollback();
            throw new Exception("failed create new transaction : " + e.InnerException.Message);
        }
    }
}