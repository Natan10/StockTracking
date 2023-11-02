using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using StockTracking.DTOs;
using StockTracking.DTOs.Stock;
using StockTracking.Models;
using StockTracking.Services.Stock;

namespace StockTracking.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
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

    }
}
