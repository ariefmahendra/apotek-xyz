namespace ariefmahendra.Dtos;

public class PurchaseResponse
{
    public string Id { get; set; }
    public string NoInvoice { get; set; }
    public string TransactionDate { get; set; }
    public long Total { get; set; }
    public List<PurchaseDetailResponse> PurchaseDetailResponses { get; set; }
}