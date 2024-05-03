using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text.Json.Serialization;

namespace ariefmahendra.Entities;

[Table(("tx_purchase_detail"))]
public class PurchaseDetail
{
    [JsonIgnore]
    [Key, Column("id")]
    public Guid Id { get; set; }

    [JsonIgnore]
    [Column("purchase_id")]
    public Guid PurchaseId { get; set; }

    [Column("product_id")]
    public Guid ProductId { get; set; }

    [Column("quantity")]
    public int Quantity { get; set; }

    [JsonIgnore]
    public Product? Product { get; set; }
}