using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text.Json.Serialization;


namespace ariefmahendra.Entities;

[Table("mst_product")]
public class Product
{
    
    [JsonIgnore]
    [Key, Column("id")]
    public Guid Id { get; set; }

    [Column("product_name")]
    public string ProductName { get; set; }

    [Column("product_code")]
    public string ProductCode { get; set; }

    [Column("product_price")]
    public long ProductPrice { get; set; }

    [Column("stock")]
    public int Stock { get; set; }

    public override string ToString()
    {
        return
            $"{nameof(Id)}: {Id}, {nameof(ProductName)}: {ProductName}, {nameof(ProductCode)}: {ProductCode}, {nameof(ProductPrice)}: {ProductPrice}, {nameof(Stock)}: {Stock}";
    }
}