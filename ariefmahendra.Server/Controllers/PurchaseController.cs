using System.Net;
using ariefmahendra.Entities;
using ariefmahendra.Services;
using ariefmahendra.Utils.CustomException;
using ariefmahendra.Utils.MapObject;
using Microsoft.AspNetCore.Mvc;

namespace ariefmahendra.Controllers;

[ApiController]
[Route("/api/v1/transactions")]
public class PurchaseController : ControllerBase
{
    private readonly IPurchaseService _purchaseService;

    public PurchaseController(IPurchaseService purchaseService)
    {
        _purchaseService = purchaseService;
    }


    [HttpPost]
    public async Task<IActionResult> CreateNewTransaction([FromBody] Purchase payload)
    {
        try
        {
            var newTransaction = await _purchaseService.CreateNewTransaction(payload);
            return Created("api/v1/transactions", Map.MapToResponse(HttpStatusCode.Created, "success create new transaction", newTransaction));
        }
        catch (NotFoundException e)
        {
            return NotFound(Map.MapToResponse(HttpStatusCode.NotFound, "product not found", null));
        }
        catch (Exception e)
        {
            Console.WriteLine("Error : " + e);
            return StatusCode(500,
                Map.MapToResponse(HttpStatusCode.InternalServerError, e.Message, null));
        }    
    }
}