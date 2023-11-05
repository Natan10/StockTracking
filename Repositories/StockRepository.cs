using Microsoft.EntityFrameworkCore;
using StockTracking.Data;
using StockTracking.DTOs.Stock;
using StockTracking.Models;
using StockTracking.Repositories.Exceptions;

namespace StockTracking.Repositories
{
    public interface IStockRepository
    {
        public Task<Stock> CreateStock(CreateStockDTO stock);
        public Task DeleteStock(int stockId);

        public Task<StockItem> CreateStockItem(CreateStockItemDTO stockItem);

        public Task<StockItem> UpdateStockItem(int stockItemId, UpdateStockItemDTO updateStockItem);

        public Task<StockItem> GetStockItemById(int stockItemId);

        public Task DeleteStockItem(int stockItemId);

        public Task<(int totalPages, List<StockItem> stockItems)> GetAllStockItems(int currentPage, int numberOfRecordPerPage);
    }

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

        public async Task DeleteStock(int stockId)
        {
            var stock = await _context.Stocks.FirstOrDefaultAsync(e => e.Id == stockId) ?? throw new NotFoundException("Estoque não encontrado");
            _context.Stocks.Remove(stock);
            await _context.SaveChangesAsync();
        }


        public async Task<StockItem> CreateStockItem(CreateStockItemDTO newStockItem)
        {
            var newItem = new StockItem
            {
                Name = newStockItem.Name,
                Type = newStockItem.Type,
                Quantity = newStockItem.Quantity,
                Code = newStockItem.Code,
                StockId = newStockItem.StockId
            };

            _context.StockItems.Add(newItem);
            await _context.SaveChangesAsync();

            var stockItem = await _context.StockItems.FirstOrDefaultAsync(e => e.Code == newStockItem.Code);

            return stockItem;
        }

        public async Task<StockItem> UpdateStockItem(int stockItemId, UpdateStockItemDTO updateStockItem)
        {
            var stockItem = await _context.StockItems.FirstOrDefaultAsync(e => e.Id == stockItemId) ?? throw new NotFoundException("Item não encontrado no estoque");
            
            _context.Entry(stockItem).CurrentValues.SetValues(updateStockItem);

            await _context.SaveChangesAsync();

            return stockItem;
        }

        public async Task DeleteStockItem(int stockItemId)
        {
            var stockItem = await _context.StockItems.FirstOrDefaultAsync(e => e.Id == stockItemId) ?? throw new NotFoundException("Item não encontrado no estoque");
            var remove = _context.StockItems.Remove(stockItem);
            await _context.SaveChangesAsync();
        }

        public async Task<(int totalPages, List<StockItem> stockItems)> GetAllStockItems(int currentPage, int numberOfRecordPerPage)
        {
            var totalPages = _context.StockItems.Count() / numberOfRecordPerPage;

            var stockItems = await _context.StockItems
                .Skip((currentPage - 1) * numberOfRecordPerPage)
                .Take(numberOfRecordPerPage)
                .ToListAsync();

            return (totalPages, stockItems);
        }

        public async Task<StockItem> GetStockItemById(int stockItemId)
        {
            var stockItem = await _context.StockItems.FirstOrDefaultAsync(e => e.Id == stockItemId) ?? throw new NotFoundException("Item não encontrado no estoque");

            return stockItem;
        }
    }
}
