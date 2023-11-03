using StockTracking.DTOs;
using StockTracking.DTOs.Stock;
using StockTracking.Repositories;
using AutoMapper;

namespace StockTracking.Services.Stock
{
    public interface IStockService
    {
        public Task<ServiceResponse<StockDTO>> CreateStock(CreateStockDTO newStock);
        public Task<ServiceResponse<bool>> DeleteStock(int stockId);

        public Task<ServiceResponse<StockItemDTO>> CreateStockItem(CreateStockItemDTO newStockItem);

        public Task<ServiceResponse<StockItemDTO>> UpdateStockItem(int stockItemId, UpdateStockItemDTO stockItem);

        public Task<ServiceResponse<bool>> DeleteStockItem(int stockItemId);

        public Task<Pagination<StockItemDTO>> GetAllStockItems(int currentPage, int numberOfRecordPerPage);
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

                var stockMapper = _mapper.Map<StockDTO>(stock);

                List<StockItemDTO> stockItems = stock.StockItems?.Select(stockItem => _mapper.Map<StockItemDTO>(stockItem)).ToList();
                
                stockMapper.StockItems = stockItems;

                serviceResponse.Data = stockMapper; 
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

        public async Task<ServiceResponse<StockItemDTO>> CreateStockItem(CreateStockItemDTO newStockItem)
        {
            var serviceResponse = new ServiceResponse<StockItemDTO>();
            try
            {
               var stockItem = await _stockRepository.CreateStockItem(newStockItem);
               
               serviceResponse.Data = _mapper.Map<StockItemDTO>(stockItem);
               serviceResponse.Success = true;

               return serviceResponse;
            }catch(Exception ex)
            {
                serviceResponse.Errors = new[] { ex.Message };
                return serviceResponse;
            }
        }

        public async Task<ServiceResponse<StockItemDTO>> UpdateStockItem(int stockItemId, UpdateStockItemDTO stockItem)
        {
            var serviceResponse = new ServiceResponse<StockItemDTO>();
            try
            {
                var updateStockItem = await _stockRepository.UpdateStockItem(stockItemId, stockItem);

                serviceResponse.Success = true;
                serviceResponse.Data = _mapper.Map<StockItemDTO>(updateStockItem);

                return serviceResponse;
            }catch(Exception ex)
            {
                serviceResponse.Errors = new[] { ex.Message };
                return serviceResponse;
            }
        }

        public async Task<ServiceResponse<bool>> DeleteStockItem(int stockItemId)
        {
            var serviceResponse = new ServiceResponse<bool>();
            try
            {
                await _stockRepository.DeleteStockItem(stockItemId);
                serviceResponse.Success = true;
                return serviceResponse;
            } catch(Exception ex)
            {
                serviceResponse.Errors = new[] { ex.Message };
                return serviceResponse;
            }
        }

        public async Task<Pagination<StockItemDTO>> GetAllStockItems(int currentPage, int numberOfRecordPerPage)
        {
            var repositoryResponse = await _stockRepository.GetAllStockItems(currentPage, numberOfRecordPerPage);

            //var stockItems = repositoryResponse.stockItems.Select(e => _mapper.Map<StockItemDTO>(e)).ToList();
            var stockItems = _mapper.Map<List<StockItemDTO>>(repositoryResponse.stockItems);

            return new Pagination<StockItemDTO>
            {
                CurrentPage =  currentPage,
                TotalPages = repositoryResponse.totalPages,
                Items = stockItems,
                PageSize = numberOfRecordPerPage
            };        
        }
    }
}
