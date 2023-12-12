using AutoMapper;
using StockTracking.DTOs;
using StockTracking.DTOs.Stock;
using StockTracking.Models;
using StockTracking.Repositories.Exceptions;
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


    public async Task<ServiceResponse<StockItemEquipmentDTO>> UpdateStockItemEquipment(long stockItemId, UpdateStockItemEquipmentDTO stockItem)
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

    public async Task<ServiceResponse<StockItemMaterialDTO>> UpdateStockItemMaterial(long stockItemId, UpdateStockItemMaterialDTO stockItem)
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

    public async Task<ServiceResponse<bool>> DeleteStockItem(long stockId, long stockItemId, EStockItemType type)
    {
        var serviceResponse = new ServiceResponse<bool>();
        try
        {
            await _stockRepository.DeleteStockItem(stockId, stockItemId, type);
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


    public async Task<ServiceResponse<StockItemEquipmentDTO>> GetStockEquipmentByParams(long stockId, long stockItemId)
    {
        var serviceResponse = new ServiceResponse<StockItemEquipmentDTO>();
        try
        {
            var stockItem = await _stockRepository.GetStockEquipmentByParams(stockId,stockItemId);

            serviceResponse.Success = true;
            serviceResponse.Data = _mapper.Map<StockItemEquipmentDTO>(stockItem);

            return serviceResponse;
        } catch(Exception ex)
        {
            serviceResponse.Errors = new[] { ex.Message };
            return serviceResponse;
        }
    }

    public async Task<ServiceResponse<StockItemMaterialDTO>> GetStockMaterialByParams(long stockItemId)
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

    public async Task<ServiceResponse<object>> MoveStockItem(MoveStockItemDTO payload)
    {
        var serviceResponse = new ServiceResponse<object>();
        try
        {
            if(payload.FromStockId == payload.ToStockId)
            {
                throw new Exception("O estoque de origem não pode ser igual ao estoque de destino");
            }

            var originStock = await _stockRepository.GetStockById(payload.FromStockId) ?? throw new NotFoundException("O Estoque de origem não existe");
            var destinyStock = await _stockRepository.GetStockById(payload.ToStockId) ?? throw new NotFoundException("O Estoque de destino não existe");

            StockItem itemOriginStock = payload.Type == EStockItemType.EQUIPMENT ?
                originStock.GetStockItemEquipmentBySerial(payload.Identifier) ?? throw new NotFoundException("Equipamento não encontrado no estoque") :
               originStock.GetStockItemMaterialByCode(payload.Identifier) ?? throw new NotFoundException("Material não encontrado no estoque");

            if (itemOriginStock.Quantity < payload.Quantity)
            {
                throw new Exception($"Estoque não possui a quantidade solicitada para o item");
            }
            

            StockItem itemDestinyStock = payload.Type == EStockItemType.EQUIPMENT ?
                destinyStock.GetStockItemEquipmentBySerial(payload.Identifier) : destinyStock.GetStockItemMaterialByCode(payload.Identifier) ;

            var errorMessage = @$"{(payload.Type == EStockItemType.MATERIAL ? "Material" : "Equipamento")} não encontrado no estoque de destino, realize uma nova entrada para continuar a operação.";
            if(itemDestinyStock == null)
            {
                throw new NotFoundException(errorMessage);
            }

            itemOriginStock.DecreaseQuantity(payload.Quantity);
            itemDestinyStock.IncreaseQuantity(payload.Quantity);

            await _stockRepository.UpdateStockItemEquipment(itemOriginStock.Id, itemOriginStock);
            await _stockRepository.UpdateStockItemEquipment(itemDestinyStock.Id, itemDestinyStock);
          
            serviceResponse.Success = true;
            return serviceResponse; 
        }
        catch (Exception ex)
        {
            serviceResponse.Errors = new[] { ex.Message };
            return serviceResponse;
        }
    }
}
