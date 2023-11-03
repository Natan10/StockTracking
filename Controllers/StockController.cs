using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using StockTracking.DTOs;
using StockTracking.DTOs.Stock;
using StockTracking.Services.Stock;

namespace StockTracking.Controllers
{
    [ApiController]
    [Route("[controller]")]
    [Authorize(Roles = "ADMIN")]
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


        [HttpDelete("{stockId:int}")]
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
        [Route("/[controller]/CreateStockItem")]
        public async Task<ActionResult<ServiceResponse<StockItemDTO>>> CreateStockItem(CreateStockItemDTO newStockItem)
        {
            if(!ModelState.IsValid)
            {
                var badRequestError = new ServiceResponse<StockItemDTO>
                {
                    Success = false,
                    Errors = ModelState.Values.SelectMany(e => e.Errors).Select(e => e.ErrorMessage)
                };
                return BadRequest(badRequestError);
            }
            var response = await _stockService.CreateStockItem(newStockItem);

            if(response.Success)
            {
                return Created("", response);
            }

            return BadRequest(response);
        }


        [HttpPut]
        [Route("/[controller]/UpdateStockItem/{stockItemId:int}")]
        public async Task<ActionResult<ServiceResponse<StockItemDTO>>> UpdateStockItem(int stockItemId, [FromBody] UpdateStockItemDTO updateStockItem)
        {
            var response = await _stockService.UpdateStockItem(stockItemId, updateStockItem);

            if(response.Success)
            {
                return NoContent();
            }
            
            return BadRequest(response);
        }

        [HttpDelete]
        [Route("/[controller]/DeleteStockItem/{stockItemId:int}")]
        public async Task<ActionResult<ServiceResponse<bool>>> DeleteStockItem(int stockItemId)
        {
            var response = await _stockService.DeleteStockItem(stockItemId);

            if (response.Success)
            {
                return NoContent();
            }

            return BadRequest(response);
        }


        [HttpGet]
        [Route("/[controller]/StockItem")]
        public async Task<ActionResult<Pagination<StockItemDTO>>> GetAllStockItems([FromQuery] int currentPage = 1, [FromQuery] int pageSize = 10)
        {
            var serviceResponse = await _stockService.GetAllStockItems(currentPage, pageSize);

            return Ok(serviceResponse);
        }
    }
}
