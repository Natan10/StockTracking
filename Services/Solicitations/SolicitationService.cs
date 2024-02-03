using AutoMapper;
using StockTracking.DTOs;
using StockTracking.DTOs.Solicitation;
using StockTracking.Models;
using StockTracking.Repositories;

namespace StockTracking.Services.Solicitations
{
    public interface ISolicitationService
    {
        Task<ServiceResponse<SolicitationDTO>> CreateSolicitation(CreateSolicitationDTO solicitationDTO);

        Task<Pagination<SolicitationDTO>> GetAllSolicitations(int currentPage, int numberOfRecordPerPage);

        Task<ServiceResponse<SolicitationDTO>> CancelSolicitation(int solicitationId, string reviewerId);
    }

    public class SolicitationService : ISolicitationService
    {
        private readonly ISolicitationRepository _solicitationRepository;
        private readonly IMapper _mapper;

        public SolicitationService(ISolicitationRepository solicitationRepository, IMapper mapper)
        {
            _solicitationRepository = solicitationRepository;
            _mapper = mapper;
        }

        public async Task<ServiceResponse<SolicitationDTO>> CancelSolicitation(int solicitationId, string reviewerId)
        {
            var serviceResponse = new ServiceResponse<SolicitationDTO>();
            try
            {
                var solicitation = await _solicitationRepository.CancelSolicitation(solicitationId, reviewerId);

                serviceResponse.Success = true;
                serviceResponse.Data = _mapper.Map<SolicitationDTO>(solicitation);

                return serviceResponse;
            }catch(Exception ex) {
                serviceResponse.Errors = new[] {ex.Message};
                return serviceResponse;
            }
        }

        public async Task<ServiceResponse<SolicitationDTO>> CreateSolicitation(CreateSolicitationDTO solicitationDTO)
        {
            var serviceResponse = new ServiceResponse<SolicitationDTO>();
            try
            {
                var solicitationItems = solicitationDTO.SolicitationItems.Select(si =>
                     _mapper.Map<SolicitationItem>(si)).ToList();

                var newSolicitation = new Solicitation
                {
                    RequesterId = solicitationDTO.RequesterId,
                    ReviewerId = solicitationDTO.ReviewerId,
                    Status = solicitationDTO.Status,
                    StockId = solicitationDTO.StockId,
                    SolicitationItems = solicitationItems
                };
                var createdSolicitation = await _solicitationRepository.CreateSolicitation(newSolicitation);
                
                var data = _mapper.Map<SolicitationDTO>(createdSolicitation);
                
                serviceResponse.Success = true;
                serviceResponse.Data = data;

                return serviceResponse;
            } catch (Exception ex)
            {
                serviceResponse.Errors = new[] { ex.Message };
                return serviceResponse;
            }
        }

        public async Task<Pagination<SolicitationDTO>> GetAllSolicitations(int currentPage, int numberOfRecordPerPage)
        {
            var repositoryResponse = await _solicitationRepository.GetAllSolicitations(currentPage, numberOfRecordPerPage);

            var solicitations = _mapper.Map<List<SolicitationDTO>>(repositoryResponse.solicitations);

            return new Pagination<SolicitationDTO>
            {
                CurrentPage = currentPage,
                TotalPages = repositoryResponse.totalPages,
                Items = solicitations,
                PageSize = numberOfRecordPerPage
            };
        }
    }
}
