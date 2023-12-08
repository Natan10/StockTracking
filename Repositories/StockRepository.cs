using Microsoft.EntityFrameworkCore;

using StockTracking.Data;
using StockTracking.DTOs.Stock;
using StockTracking.Models;
using StockTracking.Repositories.Exceptions;
using StockTracking.Repositories.Interfaces;

namespace StockTracking.Repositories;

public class StockRepository : IStockRepository
{
    private readonly DataContext _context;

    public StockRepository(DataContext context)
    {
        _context = context;
    }

    public async Task<Stock> CreateStock(CreateStockDTO newStock)
    {
        var stock = new Stock
        {
            Name = newStock.Name,
            Description = newStock.Description,
        };

        _context.Stocks.Add(stock);
        await _context.SaveChangesAsync();

        var createdStock = await _context.Stocks.FirstOrDefaultAsync(e => e.Name == newStock.Name);

        return createdStock;
    }

    public async Task DeleteStock(long stockId)
    {
        var stock = await _context.Stocks.FirstOrDefaultAsync(e => e.Id == stockId) ?? throw new NotFoundException("Estoque não encontrado");
        _context.Stocks.Remove(stock);
        await _context.SaveChangesAsync();
    }


    public async Task<StockItemEquipment> CreateStockItemEquipment(StockItemEquipment newStockItemEquipment)
    {  
        _context.StockItemEquipments.Add(newStockItemEquipment);
        await _context.SaveChangesAsync();

        var stockItem = await _context.StockItemEquipments.FirstOrDefaultAsync(e => e.Id == newStockItemEquipment.Id);

        return stockItem;
    }

    public async Task<StockItemMaterial> CreateStockItemMaterial(StockItemMaterial newStockItemMaterial)
    {
        _context.StockItemMaterials.Add(newStockItemMaterial);
        await _context.SaveChangesAsync();

        var stockItem = await _context.StockItemMaterials.FirstOrDefaultAsync(e => e.Id == newStockItemMaterial.Id);

        return stockItem;
    }


    public async Task<StockItemEquipment> UpdateStockItemEquipment(long stockItemId, object updateStockItem)
    {
        var stockItem = await _context.StockItemEquipments.FirstOrDefaultAsync(e => e.Id == stockItemId) ?? throw new NotFoundException("Equipamento não encontrado no estoque");
        
        _context.Entry(stockItem).CurrentValues.SetValues(updateStockItem);

        await _context.SaveChangesAsync();

        return stockItem;
    }

    public async Task<StockItemMaterial> UpdateStockItemMaterial(long stockItemId, object updateStockItem)
    {
        var stockItem = await _context.StockItemMaterials.FirstOrDefaultAsync(e => e.Id == stockItemId) ?? throw new NotFoundException("Material não encontrado no estoque");

        _context.Entry(stockItem).CurrentValues.SetValues(updateStockItem);

        await _context.SaveChangesAsync();

        return stockItem;
    }
   
    public async Task DeleteStockItem(long stockItemId, EStockItemType type)
    {
        if(type == EStockItemType.MATERIAL)
        {
            var stockItemMaterial = await _context.StockItemMaterials.FirstOrDefaultAsync(e => e.Id == stockItemId) ?? throw new NotFoundException("Item não encontrado no estoque");
            _context.StockItemMaterials.Remove(stockItemMaterial);
        } else
        {
            var stockItemEquipment = await _context.StockItemEquipments.FirstOrDefaultAsync(e => e.Id == stockItemId) ?? throw new NotFoundException("Item não encontrado no estoque");
            _context.StockItemEquipments.Remove(stockItemEquipment);
        }

        await _context.SaveChangesAsync();
    }

    public async Task<StockItemEquipment> GetStockEquipmentByParams(long stockItemId)
    {
        var stockItem = await _context.StockItemEquipments.FirstOrDefaultAsync(e => e.Id == stockItemId) ?? throw new NotFoundException("Equipamento não encontrado no estoque");
        return stockItem;
    }

    public async Task<StockItemMaterial> GetStockMaterialByParams(long stockItemId)
    {
        var stockItem = await _context.StockItemMaterials.FirstOrDefaultAsync(e => e.Id == stockItemId) ?? throw new NotFoundException("Material não encontrado no estoque");
        return stockItem;
    }

    public async Task<(int totalPages, List<StockItemEquipment> stockItems)> GetAllStockEquipments(int currentPage, int numberOfRecordPerPage)
    {
        var totalPages = _context.StockItemEquipments.Count() / numberOfRecordPerPage;

        var stockItems = await _context.StockItemEquipments
            .Skip((currentPage - 1) * numberOfRecordPerPage)
            .Take(numberOfRecordPerPage)
            .ToListAsync();

        return (totalPages, stockItems);
    }

    public async Task<(int totalPages, List<StockItemMaterial> stockItems)> GetAllStockMaterials(int currentPage, int numberOfRecordPerPage)
    {
        var totalPages = _context.StockItemMaterials.Count() / numberOfRecordPerPage;

        var stockItems = await _context.StockItemMaterials
            .Skip((currentPage - 1) * numberOfRecordPerPage)
            .Take(numberOfRecordPerPage)
            .ToListAsync();

        return (totalPages, stockItems);
    }

}
