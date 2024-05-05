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
    private readonly ILogger<PurchaseController> _logger;
    
    public PurchaseController(IPurchaseService purchaseService, ILogger<PurchaseController> logger)
    {
        _purchaseService = purchaseService;
        _logger = logger;
    }

    [HttpPost]
    public async Task<IActionResult> CreateNewTransaction([FromBody] Purchase payload)
    {
        try
        {
            var newTransaction = await _purchaseService.CreateNewTransaction(payload);
            return Created("api/v1/transactions",
                Map.MapToResponse(HttpStatusCode.Created, "success create new transaction", newTransaction));
        }
        catch (NotFoundException e)
        {
            _logger.LogError(e.Message);
            return NotFound(Map.MapToResponse(HttpStatusCode.NotFound, e.Message, null));
        }
        catch (BadRequestException e)
        {
            _logger.LogError(e.Message);
            return BadRequest(Map.MapToResponse(HttpStatusCode.BadRequest, e.Message, null));
        }
        catch (Exception e)
        {
            _logger.LogError(e.Message);
            return StatusCode(500,
                Map.MapToResponse(HttpStatusCode.InternalServerError, e.Message,
                    null));
        }
    }
}