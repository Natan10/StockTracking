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
    }
}
