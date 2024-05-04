using ariefmahendra.Entities;

namespace ariefmahendra.Dtos;

public class PurchaseDetailResponse
{
    public string Id { get; set; }
    public int Quantity { get; set; }
    public Product Product { get; set; }
}