using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using StockTracking.DTOs;
using StockTracking.DTOs.Solicitation;
using StockTracking.Services.Solicitations;

namespace StockTracking.Controllers
{
    [ApiController]
    [Route("[controller]")]
    //[Authorize(Roles = "ADMIN")]
    public class SolicitationController : ControllerBase
    {
        private readonly ISolicitationService _solicitationService;

        public SolicitationController(ISolicitationService solicitationService)
        {
            _solicitationService = solicitationService;
        }

        [HttpPost]
        public async Task<ActionResult<ServiceResponse<SolicitationDTO>>> CreateSolicitation(CreateSolicitationDTO createSolicitationDTO) {

           var response = await _solicitationService.CreateSolicitation(createSolicitationDTO);

           if(response.Success)
            {
                return Created("", response);
            }

           return BadRequest(response);
        }

    }
}
