using StockTracking.DTOs;
using StockTracking.DTOs.Stock;
using StockTracking.Models;

namespace StockTracking.Services.Stock.Interfaces;

public interface IStockService
{
    public Task<ServiceResponse<StockDTO>> CreateStock(CreateStockDTO newStock);

    public Task<ServiceResponse<bool>> DeleteStock(int stockId);

    public Task<ServiceResponse<StockItemEquipmentDTO>> CreateStockItemEquipment(CreateStockItemEquipmentDTO newStockItem);

    public Task<ServiceResponse<StockItemMaterialDTO>> CreateStockItemMaterial(CreateStockItemMaterialDTO newStockItem);

    public Task<ServiceResponse<StockItemEquipmentDTO>> UpdateStockItemEquipment(int stockItemId, UpdateStockItemEquipmentDTO stockItem);

    public Task<ServiceResponse<StockItemMaterialDTO>> UpdateStockItemMaterial(int stockItemId, UpdateStockItemMaterialDTO stockItem);


    public Task<ServiceResponse<bool>> DeleteStockItem(int stockItemId, EStockItemType type);

    public Task<ServiceResponse<StockItemEquipmentDTO>> GetStockEquipmentByParams(int stockItemId);

    public Task<ServiceResponse<StockItemMaterialDTO>> GetStockMaterialByParams(int stockItemId);

    public Task<Pagination<StockItemEquipmentDTO>> GetAllEquipments(int currentPage, int numberOfRecordPerPage);

    public Task<Pagination<StockItemMaterialDTO>> GetAllMaterials(int currentPage, int numberOfRecordPerPage);
}
