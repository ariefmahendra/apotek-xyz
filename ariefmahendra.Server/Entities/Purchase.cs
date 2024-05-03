using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text.Json.Serialization;

namespace ariefmahendra.Entities;

[Table("tx_purchase")]
public class Purchase
{
    [JsonIgnore]
    [Key, Column("id")]
    public Guid Id { get; set; }
    
    [Column("no_invoice")]
    public string NoInvoice { get; set; }
    
    [Column("transaction_date")]
    public DateTime TransactionDate { get; set; }

    [JsonIgnore]
    [Column("total")]
    public long Total { get; set; }
    
    public List<PurchaseDetail>? PurchaseDetails { get; set; }
}