using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http.HttpResults;
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

        [HttpGet]
        public async Task<ActionResult<Pagination<SolicitationDTO>>> GetAllSolicitations([FromQuery] int currentPage = 1, [FromQuery] int pageSize = 10)
        {
            var solicitations = await _solicitationService.GetAllSolicitations(currentPage, pageSize);
            return Ok(solicitations);
        }

    }
}
