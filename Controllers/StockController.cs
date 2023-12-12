using Microsoft.AspNetCore.Authorization;

using Microsoft.AspNetCore.Mvc;
using StockTracking.DTOs;
using StockTracking.DTOs.Stock;
using StockTracking.Models;
using StockTracking.Services.Stock.Interfaces;

namespace StockTracking.Controllers;

[ApiController]
[Route("[controller]")]
//[Authorize(Roles = "ADMIN")]
public class StockController : ControllerBase
{
    private readonly IStockService _stockService;

    public StockController(IStockService stockService)
    {
        _stockService = stockService;
    }


    [HttpPost]
    public async Task<ActionResult<ServiceResponse<StockDTO>>> CreateStock(CreateStockDTO newStock)
    {
        var response = await _stockService.CreateStock(newStock);

        return Created("", response);
    }


    [HttpDelete("{stockId:long}")]
    public async Task<ActionResult> DeleteStock(int stockId)
    {
        var response = await _stockService.DeleteStock(stockId);

        if(response.Success)
        {
            return NoContent();
        }

        return BadRequest(response);
    }

    
    [HttpPost]
    [Route("/[controller]/CreateStockItemEquipment")]
    public async Task<ActionResult<ServiceResponse<StockItemEquipmentDTO>>> CreateStockItemEquipment(CreateStockItemEquipmentDTO newEquipment)
    {
        if(!ModelState.IsValid)
        {
            var badRequestError = new ServiceResponse<StockItemEquipmentDTO>
            {
                Success = false,
                Errors = ModelState.Values.SelectMany(e => e.Errors).Select(e => e.ErrorMessage)
            };
            return BadRequest(badRequestError);
        }
        var response = await _stockService.CreateStockItemEquipment(newEquipment);

        if(response.Success)
        {
            return Created("", response);
        }

        return BadRequest(response);
    }

    [HttpPost]
    [Route("/[controller]/CreateStockItemMaterial")]
    public async Task<ActionResult<ServiceResponse<StockItemMaterialDTO>>> CreateStockItemMaterial(CreateStockItemMaterialDTO newMaterial)
    {
        if (!ModelState.IsValid)
        {
            var badRequestError = new ServiceResponse<StockItemMaterialDTO>
            {
                Success = false,
                Errors = ModelState.Values.SelectMany(e => e.Errors).Select(e => e.ErrorMessage)
            };
            return BadRequest(badRequestError);
        }
        var response = await _stockService.CreateStockItemMaterial(newMaterial);

        if (response.Success)
        {
            return Created("", response);
        }

        return BadRequest(response);
    }


    [HttpPut]
    [Route("/[controller]/UpdateStockItemMaterial/{stockItemMaterialId:long}")]
    public async Task<ActionResult<ServiceResponse<StockItemMaterialDTO>>> UpdateStockItemMaterial(int stockItemMaterialId, [FromBody] UpdateStockItemMaterialDTO updateStockItem)
    {
        var response = await _stockService.UpdateStockItemMaterial(stockItemMaterialId, updateStockItem);

        if(response.Success)
        {
            return NoContent();
        }
        
        return BadRequest(response);
    }

    [HttpPut]
    [Route("/[controller]/UpdateStockItemEquipment/{stockItemEquipmentId:long}")]
    public async Task<ActionResult<ServiceResponse<StockItemEquipmentDTO>>> UpdateStockItemEquipment(int stockItemEquipmentId, [FromBody] UpdateStockItemEquipmentDTO updateStockItem)
    {
        var response = await _stockService.UpdateStockItemEquipment(stockItemEquipmentId, updateStockItem);

        if (response.Success)
        {
            return NoContent();
        }

        return BadRequest(response);
    }


    
    [HttpDelete]
    [Route("/[controller]/DeleteStockItem/{stockId}/{stockItemId}")]
    public async Task<ActionResult<ServiceResponse<bool>>> DeleteStockItem(long stockId,long stockItemId, [FromQuery(Name = "type")] EStockItemType type)
    {
        var response = await _stockService.DeleteStockItem(stockId, stockItemId, type);

        if (response.Success)
        {
            return NoContent();
        }

        return BadRequest(response);
    }

    
    [HttpGet]
    [Route("/[controller]/GetAllMaterials")]
    public async Task<ActionResult<Pagination<StockItemMaterialDTO>>> GetAllMaterials([FromQuery] int currentPage = 1, [FromQuery] int pageSize = 10)
    {
        var response = await _stockService.GetAllMaterials(currentPage, pageSize);

        return Ok(response);
    }

    [HttpGet]
    [Route("/[controller]/GetAllEquipments")]
    public async Task<ActionResult<Pagination<StockItemEquipmentDTO>>> GetAllEquipments([FromQuery] int currentPage = 1, [FromQuery] int pageSize = 10)
    {
        var response = await _stockService.GetAllEquipments(currentPage, pageSize);

        return Ok(response);
    }


    [HttpGet]
    [Route("/[controller]/GetStockItemMaterialById/{stockItemId:long}")]
    public async Task<ActionResult<ServiceResponse<StockItemMaterialDTO>>> GetStockItemMaterialById(int stockItemId)
    {
        var response = await _stockService.GetStockMaterialByParams(stockItemId);

        if(response.Success)
        {
            return Ok(response);
        }

        return NotFound(response);
    }

    [HttpGet("/[controller]/GetStockItemEquipmentById/{stockId}/{stockItemId}")]
    public async Task<ActionResult<ServiceResponse<StockItemMaterialDTO>>> GetStockItemEquipmentById(int stockId, int stockItemId)
    {
        var response = await _stockService.GetStockEquipmentByParams(stockId,stockItemId);

        if (response.Success)
        {
            return Ok(response);
        }

        return NotFound(response);
    }


    [HttpPost("/[controller]/MoveStockItem")]
    public async Task<ActionResult<ServiceResponse<object>>> MoveStockItem([FromBody] MoveStockItemDTO payload)
    {
        var response = await _stockService.MoveStockItem(payload);

        if (response.Success)
        {
            return NoContent();
        }

        return BadRequest(response);
    }

}
