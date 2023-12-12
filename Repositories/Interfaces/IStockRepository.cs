using StockTracking.DTOs.Stock;
using StockTracking.Models;

namespace StockTracking.Repositories.Interfaces;

public interface IStockRepository
{
    public Task<Stock> GetStockById(long id);

    public Task<Stock> CreateStock(CreateStockDTO stock);
    
    public Task DeleteStock(long stockId);

    public Task<StockItemEquipment> CreateStockItemEquipment(StockItemEquipment newStockItem);

    public Task<StockItemMaterial> CreateStockItemMaterial(StockItemMaterial newStockItemMaterial);

    public Task<StockItemEquipment> UpdateStockItemEquipment(long stockItemId, object updateStockItem);

    public Task<StockItemMaterial> UpdateStockItemMaterial(long stockItemId, object updateStockItem);

    public Task DeleteStockItem(long stockId, long stockItemId, EStockItemType type);

    public Task<StockItemEquipment> GetStockEquipmentByParams(long stockId, long stockItemId);

    public Task<StockItemMaterial> GetStockMaterialByParams(long stockItemId);

    public Task<(int totalPages, List<StockItemEquipment> stockItems)> GetAllStockEquipments(int currentPage, int numberOfRecordPerPage);

    public Task<(int totalPages, List<StockItemMaterial> stockItems)> GetAllStockMaterials(int currentPage, int numberOfRecordPerPage);
}
