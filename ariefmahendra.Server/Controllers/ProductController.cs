using System.Net;
using ariefmahendra.Entities;
using ariefmahendra.Services;
using ariefmahendra.Utils.CustomException;
using ariefmahendra.Utils.MapObject;
using Microsoft.AspNetCore.Mvc;


namespace ariefmahendra.Controllers;

[ApiController]
[Route("/api/v1/products")]
public class ProductController: ControllerBase
{

    private readonly IProductService _productService;

    public ProductController(IProductService productService)
    {
        _productService = productService;
    }
    
    [HttpPost]
    public async Task<IActionResult> CreateProduct([FromBody] Product payload)
    {

        try
        {
            var product = await _productService.Create(payload);
            var response = Map.MapToResponse(HttpStatusCode.Created, "success create new product", product);
            return Created("/api/v1/products", response);
        }
        catch (Exception e)
        {
            return StatusCode(500,
                Map.MapToResponse(HttpStatusCode.InternalServerError, "error while creating product", null));
        }
        
    }

    [HttpGet("{id}")]
    public async Task<IActionResult> GetProductById(string? id)
    {
        try
        {
            var byId = await _productService.GetById(id);
            var response = Map.MapToResponse(HttpStatusCode.OK, "success get product by id", byId);
            return Ok(response);
        }
        catch (NotFoundException e)
        {
            return NotFound(Map.MapToResponse(HttpStatusCode.NotFound, "product not found", null));
        }
        catch (Exception e)
        {
            Console.WriteLine("Error : " + e.Message);
            return StatusCode(500,
                Map.MapToResponse(HttpStatusCode.InternalServerError, "error while get product", null));
        }
    }

    [HttpGet]
    public async Task<IActionResult> GetAllProduct()
    {
        var products = await _productService.GetAll();
        var response = Map.MapToResponse(HttpStatusCode.OK, "success get all product", products);
        return Ok(response);
    }

    [HttpPut("{id}")]
    public async Task<IActionResult> UpdateProductById([FromBody] Product payload, string id)
    {
        var product = new Product()
        {
            Id = Guid.Parse(id),
            ProductName = payload.ProductName,
            ProductCode = payload.ProductCode,
            ProductPrice = payload.ProductPrice,
            Stock = payload.Stock
        };
        try
        {
            var productUpdated = await _productService.Update(product);
            var response = Map.MapToResponse(HttpStatusCode.OK, "success update product", productUpdated);
            return Ok(response);
        }
        catch (NotFoundException e)
        {
            return NotFound(Map.MapToResponse(HttpStatusCode.NotFound, "product not found", null));
        }
        catch (Exception e)
        {
            Console.WriteLine("Error : " + e.Message);
            return StatusCode(500,
                Map.MapToResponse(HttpStatusCode.InternalServerError, "error while update product", null));
        }
    }

    [HttpDelete("{id}")]
    public async Task<IActionResult> DeleteProductById(string? id)
    {
        try
        {
            await _productService.DeleteById(id);
            return Ok(Map.MapToResponse(HttpStatusCode.OK, "success delete product", null));
        }
        catch (NotFoundException e)
        {
            return NotFound(Map.MapToResponse(HttpStatusCode.NotFound, "product not found", null));
        }
        catch (Exception e)
        {
            Console.WriteLine("Error : " + e.Message);
            return StatusCode(500,
                Map.MapToResponse(HttpStatusCode.InternalServerError, "error while update product", null));
        }       
    }
    
}