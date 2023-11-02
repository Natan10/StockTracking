using StockTracking.DTOs;
using StockTracking.DTOs.Stock;
using StockTracking.Repositories;
using StockTracking.Models;
using AutoMapper;

namespace StockTracking.Services.Stock
{
    public interface IStockService
    {
        public Task<ServiceResponse<StockDTO>> CreateStock(CreateStockDTO newStock);
        public Task<ServiceResponse<Boolean>> DeleteStock(int stockId);
    }

    public class StockService : IStockService
    {
        private readonly IStockRepository _stockRepository;
        private readonly IMapper _mapper;

        public StockService(IStockRepository stockRepository, IMapper mapper)
        {
            _stockRepository = stockRepository;
            _mapper = mapper;
        }
        public async Task<ServiceResponse<StockDTO>> CreateStock(CreateStockDTO newStock)
        {   
            var serviceResponse = new ServiceResponse<StockDTO>();
            try
            {
                var stock = await _stockRepository.CreateStock(newStock);
            
                serviceResponse.Data = _mapper.Map<StockDTO>(stock);
                serviceResponse.Success = true;

                return serviceResponse;

            }catch(Exception ex)
            {
                serviceResponse.Errors = new[] { ex.Message };
                return serviceResponse;
            }
        }

        public async Task<ServiceResponse<Boolean>> DeleteStock(int stockId)
        {
            var serviceResponse = new ServiceResponse<Boolean>();
            try
            {
                await _stockRepository.DeleteStock(stockId);

                serviceResponse.Success = true;
                return serviceResponse;
            }catch(Exception e)
            {
                serviceResponse.Errors = new[] { e.Message };
                return serviceResponse;
            }
        }
    }
}
