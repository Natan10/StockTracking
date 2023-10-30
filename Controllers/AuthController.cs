using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using StockTracking.DTOs;
using StockTracking.DTOs.Auth;
using StockTracking.Models;
using StockTracking.Services.Auth;

namespace StockTracking.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class AuthController : ControllerBase
    {
        private readonly IAuthService _authService;

        public AuthController(IAuthService authService)
        {
            _authService = authService;
        }


        [HttpPost]
        [Route("Login")]
        public async Task<ActionResult<ApiResponse<object>>> LoginEmployee(LoginEmployeeDTO loginEmployeeDTO)
        {
            var response = new ApiResponse<ApiResponse<object>>();
            if(!ModelState.IsValid)
            {
                response.Errors = ModelState.Values.SelectMany(e => e.Errors).Select(e => e.ErrorMessage);
                return BadRequest(response);
            }

            var data = await _authService.LoginEmployee(loginEmployeeDTO);

            if(data.Success)
                return Ok(data);

            if (data.Data == null)
                return NotFound(data);

            return BadRequest(data);

        }


        [HttpPost]
        [Route("Register")]
        [Authorize(Roles = "ADMIN")]
        public async Task<ActionResult<ApiResponse<Employee>>> RegisterEmployee(RegisterEmployeeDTO registerEmployee)
        {
            var response = new ApiResponse<Employee>();

            if(!ModelState.IsValid)
            {
                response.Errors = ModelState.Values.SelectMany(e => e.Errors).Select(e => e.ErrorMessage);
                response.Success = false;
                return BadRequest(response);
            }

            var data = await _authService.RegisterEmployee(registerEmployee);
           
            if(data.Success)
            {
                return Created("", data);
            }

            return BadRequest(data);
        }

    }
}
