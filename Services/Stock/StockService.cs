using AutoMapper;

using StockTracking.DTOs;
using StockTracking.DTOs.Stock;
using StockTracking.Models;
using StockTracking.Repositories.Interfaces;
using StockTracking.Services.Stock.Interfaces;

namespace StockTracking.Services.Stock;

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


    public async Task<ServiceResponse<StockItemEquipmentDTO>> CreateStockItemEquipment(CreateStockItemEquipmentDTO newStockItem)
    {
        var serviceResponse = new ServiceResponse<StockItemEquipmentDTO>();
        try
        {
            var equipment = new StockItemEquipment
            {
               Onu = newStockItem.Onu,
               Serial = newStockItem.Serial,
               Quantity = newStockItem.Quantity,
               StockId = newStockItem.StockId,
            };

           var stockItem = await _stockRepository.CreateStockItemEquipment(equipment);

           serviceResponse.Data = _mapper.Map<StockItemEquipmentDTO>(stockItem);
           serviceResponse.Success = true;

           return serviceResponse;
        }catch(Exception ex)
        {
            serviceResponse.Errors = new[] { ex.Message };
            return serviceResponse;
        }
    }

    public async Task<ServiceResponse<StockItemMaterialDTO>> CreateStockItemMaterial(CreateStockItemMaterialDTO newStockItem)
    {
        var serviceResponse = new ServiceResponse<StockItemMaterialDTO>();
        try
        {
            var material = new StockItemMaterial
            {
               Name = newStockItem.Name,
               Code = newStockItem.Code,
               Quantity = newStockItem.Quantity,
               MeasurementUnit = newStockItem.MeasurementUnit,
               StockId = newStockItem.StockId
            };

            var stockItem = await _stockRepository.CreateStockItemMaterial(material);

            serviceResponse.Data = _mapper.Map<StockItemMaterialDTO>(stockItem);
            serviceResponse.Success = true;

            return serviceResponse;
        }
        catch (Exception ex)
        {
            serviceResponse.Errors = new[] { ex.Message };
            return serviceResponse;
        }
    }


    public async Task<ServiceResponse<StockItemEquipmentDTO>> UpdateStockItemEquipment(int stockItemId, UpdateStockItemEquipmentDTO stockItem)
    {
        var serviceResponse = new ServiceResponse<StockItemEquipmentDTO>();
        try
        {
            var updateStockItem = new {
              stockItem.Onu,
              stockItem.Serial,
              stockItem.Quantity
            };

            var equipment = await _stockRepository.UpdateStockItemEquipment(stockItemId, updateStockItem);

            serviceResponse.Success = true;
            serviceResponse.Data = _mapper.Map<StockItemEquipmentDTO>(equipment);

            return serviceResponse;
        }catch(Exception ex)
        {
            serviceResponse.Errors = new[] { ex.Message };
            return serviceResponse;
        }
    }

    public async Task<ServiceResponse<StockItemMaterialDTO>> UpdateStockItemMaterial(int stockItemId, UpdateStockItemMaterialDTO stockItem)
    {
        var serviceResponse = new ServiceResponse<StockItemMaterialDTO>();
        try
        {
            var updateStockItem = new {
                stockItem.Name,
                stockItem.Code,
                stockItem.MeasurementUnit,
                stockItem.Quantity
            };

            var material = await _stockRepository.UpdateStockItemMaterial(stockItemId, updateStockItem);

            serviceResponse.Success = true;
            serviceResponse.Data = _mapper.Map<StockItemMaterialDTO>(material);

            return serviceResponse;
        }
        catch (Exception ex)
        {
            serviceResponse.Errors = new[] { ex.Message };
            return serviceResponse;
        }
    }

    public async Task<ServiceResponse<bool>> DeleteStockItem(int stockItemId, EStockItemType type)
    {
        var serviceResponse = new ServiceResponse<bool>();
        try
        {
            await _stockRepository.DeleteStockItem(stockItemId, type);
            serviceResponse.Success = true;
            return serviceResponse;
        } catch(Exception ex)
        {
            serviceResponse.Errors = new[] { ex.Message };
            return serviceResponse;
        }
    }


    public async Task<Pagination<StockItemEquipmentDTO>> GetAllEquipments(int currentPage, int numberOfRecordPerPage)
    {
        var repositoryResponse = await _stockRepository.GetAllStockEquipments(currentPage, numberOfRecordPerPage);

        var stockItems = _mapper.Map<List<StockItemEquipmentDTO>>(repositoryResponse.stockItems);

        return new Pagination<StockItemEquipmentDTO>
        {
            CurrentPage =  currentPage,
            TotalPages = repositoryResponse.totalPages,
            Items = stockItems,
            PageSize = numberOfRecordPerPage
        };        
    }

    public async Task<Pagination<StockItemMaterialDTO>> GetAllMaterials(int currentPage, int numberOfRecordPerPage)
    {
        var repositoryResponse = await _stockRepository.GetAllStockMaterials(currentPage, numberOfRecordPerPage);

        var stockItems = _mapper.Map<List<StockItemMaterialDTO>>(repositoryResponse.stockItems);

        return new Pagination<StockItemMaterialDTO>
        {
            CurrentPage = currentPage,
            TotalPages = repositoryResponse.totalPages,
            Items = stockItems,
            PageSize = numberOfRecordPerPage
        };
    }


    public async Task<ServiceResponse<StockItemEquipmentDTO>> GetStockEquipmentByParams(int stockItemId)
    {
        var serviceResponse = new ServiceResponse<StockItemEquipmentDTO>();
        try
        {
            var stockItem = await _stockRepository.GetStockEquipmentByParams(stockItemId);

            serviceResponse.Success = true;
            serviceResponse.Data = _mapper.Map<StockItemEquipmentDTO>(stockItem);

            return serviceResponse;
        } catch(Exception ex)
        {
            serviceResponse.Errors = new[] { ex.Message };
            return serviceResponse;
        }
    }

    public async Task<ServiceResponse<StockItemMaterialDTO>> GetStockMaterialByParams(int stockItemId)
    {
        var serviceResponse = new ServiceResponse<StockItemMaterialDTO>();
        try
        {
            var stockItem = await _stockRepository.GetStockMaterialByParams(stockItemId);

            serviceResponse.Success = true;
            serviceResponse.Data = _mapper.Map<StockItemMaterialDTO>(stockItem);

            return serviceResponse;
        }
        catch (Exception ex)
        {
            serviceResponse.Errors = new[] { ex.Message };
            return serviceResponse;
        }
    }
}
