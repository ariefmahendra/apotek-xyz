using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;


namespace ariefmahendra.Entities;

[Table("mst_product")]
public class Product
{
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
}