using ariefmahendra.Entities;
using Microsoft.EntityFrameworkCore.Metadata.Internal;

namespace ariefmahendra.Dtos;

public class PurchaseDetailResponse
{
    public string Id { get; set; }
    public int Quantity { get; set; }
    public Product Product { get; set; }
}