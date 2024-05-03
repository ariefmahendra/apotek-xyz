using ariefmahendra.Dtos;
using ariefmahendra.Entities;
using ariefmahendra.Repositories;
using ariefmahendra.Utils.CustomException;
using ariefmahendra.Utils.MapObject;

namespace ariefmahendra.Services;

public class PurchaseService: IPurchaseService
{

    private readonly IPersistence _persistence;
    private readonly IRepository<Purchase> _repository;
    private readonly IProductService _productService;

    public PurchaseService(IPersistence persistence, IRepository<Purchase> repository, IProductService productService)
    {
        _persistence = persistence;
        _repository = repository;
        _productService = productService;
    }

    public async Task<PurchaseResponse> CreateNewTransaction(Purchase payload)
    {
        /*
         * masukkin si purchase
         * cari produk
         * masukkin ke dalam purchase detail
         * hitung total transactionnya dan update nilai total transaction
         * return hasilnya
         */

        await _persistence.BeginTransaction();
        try
        {
            // save purchase
            var purchase = await _repository.SaveAsync(payload);
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
                    var productById = await _productService.GetById(Convert.ToString(purchaseDetail.ProductId));
                    
                    // update stock product id
                    productById.Stock -= purchaseDetail.Quantity;
                    var productUpdated = await _productService.Update(productById);
                    await _persistence.SaveChangesAsync();
                        
                    // store purchase detail to purchase detail response
                    purchaseDetails.Add(new PurchaseDetailResponse()
                    {
                        Id = purchaseDetail.Id.ToString(),
                        Product = productUpdated,
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
            _repository.Update(purchase);
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