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
    }
}
