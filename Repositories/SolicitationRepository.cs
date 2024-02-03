using Microsoft.EntityFrameworkCore;

using StockTracking.Data;
using StockTracking.Models;
using StockTracking.Repositories.Exceptions;
using StockTracking.Repositories.Interfaces;

namespace StockTracking.Repositories
{
    public interface ISolicitationRepository
    {
       Task<Solicitation> CreateSolicitation(Solicitation newSolicitation);

       Task<Solicitation> CancelSolicitation(int solicitationId, string reviewerId);

       Task<(int totalPages, List<Solicitation> solicitations)> GetAllSolicitations(int currentPage, int numberOfRecordPerPage);
    
       Task<Solicitation> GetSolicitationById(int solicitationId);
    }

    public class SolicitationRepository : ISolicitationRepository
    {
        private readonly DataContext _context;
        private readonly IStockRepository _stockRepository;

        public SolicitationRepository(DataContext context, IStockRepository stockRepository)
        {
            _context = context;
            _stockRepository = stockRepository;
        }


        public async Task<Solicitation> CancelSolicitation(int solicitationId, string reviewerId)
        {
            var reviewer = await _context.Employees.FirstOrDefaultAsync(e => e.Id == reviewerId)
                ?? throw new NotFoundException("Revisor não encontrado");
            
            var solicitation = await _context.Solicitations
                .Include(e => e.Requester)
                .Include(e => e.SolicitationItems)
                .FirstOrDefaultAsync(e => e.Id == solicitationId)
                ?? throw new NotFoundException("Solicitação não encontrada");


            solicitation.Status = ESolicitationStatus.CANCELED;
            solicitation.Reviewer = reviewer;

            _context.Solicitations.Update(solicitation);

            await _context.SaveChangesAsync();

            return solicitation;
        }

        public async Task<Solicitation> CreateSolicitation(Solicitation newSolicitation)
        {
 
            var stock = await _stockRepository.GetStockById(newSolicitation.StockId) ?? throw new NotFoundException("Estoque não encontrado");

            foreach (var solicitationItem in newSolicitation.SolicitationItems)
            {
                if(solicitationItem.EquipmentId != null)
                {
                    var equipment = stock.StockItemEquipments.FirstOrDefault(e => e.Id == solicitationItem.EquipmentId) ?? throw new NotFoundException("Equipamento não encontrado no estoque");
                }

                if(solicitationItem.MaterialId != null)
                {
                    var material = stock.StockItemMaterials.FirstOrDefault(e => e.Id == solicitationItem.MaterialId) ?? throw new NotFoundException("Material não encontrado no estoque");
                }
            }

            _context.Solicitations.Add(newSolicitation);

            await _context.SaveChangesAsync();

            var createdSolicitation = await _context.Solicitations
                .Include(e => e.SolicitationItems)
                .Include(e => e.Requester)
                .FirstOrDefaultAsync(e => e.Id == newSolicitation.Id);

            return createdSolicitation;
        }

        // adicionar filtros de status, reviewer
        public async Task<(int totalPages, List<Solicitation> solicitations)> GetAllSolicitations(int currentPage, int numberOfRecordPerPage)
        {
            var totalPages = _context.Solicitations.Count() / numberOfRecordPerPage;

            var solicitations = await _context.Solicitations
                .Include(e => e.SolicitationItems)
                .Include(e => e.Requester)
                .Include(e => e.Reviewer)
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
