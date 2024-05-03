using System.Net;
using ariefmahendra.Dtos;
using ariefmahendra.Entities;
using WebResponse = ariefmahendra.Dtos.WebResponse;

namespace ariefmahendra.Utils.MapObject;

public class Map
{
    public static WebResponse MapToResponse(HttpStatusCode code, string message, object? data)
    {
        return new WebResponse()
        {
            Code = Convert.ToInt32(code),
            Message = message,
            Data = data
        };
    }

    public static ProductResponse MapProductResponse(Product product)
    {
        return new ProductResponse()
        {
            Id = product.Id.ToString(),
            ProductName = product.ProductName,
            ProductCode = product.ProductCode,
            ProductPrice = product.ProductPrice,
            Stock = product.Stock
        };
    }

    public static Product MapProductEntity(ProductResponse product)
    {
        return new Product()
        {
            Id = Guid.Parse(product.Id),
            ProductName = product.ProductName,
            ProductCode = product.ProductCode,
            ProductPrice = product.ProductPrice,
            Stock = product.Stock
        };
    }
}