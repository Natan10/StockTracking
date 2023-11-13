using AutoMapper;
using Microsoft.EntityFrameworkCore;
using StockTracking.Data;
using StockTracking.Models;
using StockTracking.Repositories.Exceptions;

namespace StockTracking.Repositories
{
    public interface ISolicitationRepository
    {
       Task<Solicitation> CreateSolicitation(Solicitation newSolicitation);

       Task CancelSolicitation(int solicitationId, string reviewerId);

       Task<(int totalPages, List<Solicitation> solicitations)> GetAllSolicitations(int currentPage, int numberOfRecordPerPage);
    
       Task<Solicitation> GetSolicitationById(int solicitationId);
    }

    public class SolicitationRepository : ISolicitationRepository
    {
        private readonly DataContext _context;
        
        public SolicitationRepository(DataContext context, IMapper mapper)
        {
            _context = context;
        }


        public async Task CancelSolicitation(int solicitationId, string reviewerId)
        {
            var reviewer = await _context.Employees.FirstOrDefaultAsync(e => e.Id == reviewerId)
                ?? throw new NotFoundException("Revisor não encontrado");
            
            var solicitation = await _context.Solicitations.FirstOrDefaultAsync(e => e.Id == solicitationId)
                ?? throw new NotFoundException("Solicitação não encontrada");


            solicitation.Status = ESolicitationStatus.CANCELED;
            solicitation.Reviewer = reviewer;

            _context.Solicitations.Update(solicitation);

            await _context.SaveChangesAsync();
        }

        public async Task<Solicitation> CreateSolicitation(Solicitation newSolicitation)
        {
            var solicitationStockItems = new List<StockItem>();

            foreach(var item in newSolicitation.SolicitationItems)
            {
                var stockItem = await _context.StockItems
                    .FirstOrDefaultAsync(e => e.Id == item.StockItemId) ?? 
                    throw new NotFoundException("Item não encontrado no estoque");
                solicitationStockItems.Add(stockItem);
            }

            _context.Solicitations.Add(newSolicitation);

            await _context.SaveChangesAsync();

            var id = newSolicitation.Id;

            var createdSolicitation = await _context.Solicitations
                .Include(e => e.SolicitationItems)
                .Include(e => e.Requester)
                .FirstOrDefaultAsync(e => e.Id == id);

            return createdSolicitation;
        }

        // adicionar filtros de status, reviewer
        public async Task<(int totalPages, List<Solicitation> solicitations)> GetAllSolicitations(int currentPage, int numberOfRecordPerPage)
        {
            var totalPages = _context.Solicitations.Count() / numberOfRecordPerPage;

            var solicitations = await _context.Solicitations
                .Include(e => e.SolicitationItems)
                .Skip((currentPage - 1) * numberOfRecordPerPage)
                .Take(numberOfRecordPerPage)
                .ToListAsync();

            return (totalPages, solicitations);
        }

        public async Task<Solicitation> GetSolicitationById(int solicitationId)
        {
            var solicitation = await _context.Solicitations.FirstOrDefaultAsync(e => e.Id == solicitationId)
                ?? throw new NotFoundException("Solicitação não encontrada");

            return solicitation;
        }
    }
}
