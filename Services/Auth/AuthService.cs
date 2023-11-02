using Microsoft.AspNetCore.Identity;
using StockTracking.DTOs;
using StockTracking.DTOs.Auth;
using StockTracking.Models;
using StockTracking.Repositories;
using System.Security.Claims;

namespace StockTracking.Services.Auth
{
    public interface IAuthService
    {
        Task<ServiceResponse<EmployeeDTO>> RegisterEmployee(RegisterEmployeeDTO registerEmployee);
        Task<ServiceResponse<object>> LoginEmployee(LoginEmployeeDTO loginEmployee);
    }

    public class AuthService : IAuthService
    {
        private readonly SignInManager<IdentityUser> _signInManager;
        private readonly UserManager<IdentityUser> _userManager;
        private readonly IEmployeeRepository _employeeRepository;
        private readonly ITokenService _tokenService;

        public AuthService(
            SignInManager<IdentityUser> signInManager,
            UserManager<IdentityUser> userManager,
            IEmployeeRepository employeeRepository,
            ITokenService tokenService
        )
        {
            _signInManager = signInManager;
            _userManager = userManager;
            _employeeRepository = employeeRepository;
            _tokenService = tokenService;
        }

        public async Task<ServiceResponse<object>> LoginEmployee(LoginEmployeeDTO loginEmployee)
        {
            var ServiceResponse = new ServiceResponse<object>();

            try
            {
                var userIdentity = await _userManager.FindByEmailAsync(loginEmployee.Email);

                
                if(userIdentity == null)
                {
                    ServiceResponse.Errors = new[] { "Funcionário não encontrado" };
                    return ServiceResponse;
                }

                var isValidPassword = await _userManager.CheckPasswordAsync(userIdentity, loginEmployee.Password);
                if (!isValidPassword)
                {
                    ServiceResponse.Errors = new[] { "Email ou senha inválidos" };
                    return ServiceResponse;
                }

                var employee = await _employeeRepository.GetEmployeeById(userIdentity.Id);

                if (employee == null)
                {
                    ServiceResponse.Errors = new[] { "Funcionário não encontrado" };
                    return ServiceResponse;
                }

                var token = _tokenService.GenerateJwtToken(employee.Id, employee.Role);

         
                ServiceResponse.Success = true;
                ServiceResponse.Data = new { Token = token };
                return ServiceResponse;
            }
            catch( Exception ex ) {
                ServiceResponse.Errors = new[] { ex.Message };
                return ServiceResponse;
            }
        }

        public async Task<ServiceResponse<EmployeeDTO>> RegisterEmployee(RegisterEmployeeDTO registerEmployee)
        {
            try
            {
                var ServiceResponse = new ServiceResponse<EmployeeDTO>();

                var user = new IdentityUser
                {
                    UserName = registerEmployee.Username ?? registerEmployee.Email,
                    Email = registerEmployee.Email,
                    EmailConfirmed = true,
                };

                var resultCreateIdentityUser = await _userManager.CreateAsync(user, registerEmployee.Password);

                var resultAddClaimRole = await _userManager.AddClaimAsync(user, new Claim("Role", registerEmployee.Role.ToString()));

                if (resultCreateIdentityUser.Succeeded && resultAddClaimRole.Succeeded)
                {
                    var createdUser = await _userManager.FindByEmailAsync(registerEmployee.Email);
                    var newEmployee = new Employee
                    {
                        Id = createdUser.Id,
                        Role = registerEmployee.Role ?? EEmployeeRole.USER,
                    };
                    var createdEmployee = await _employeeRepository.CreateEmployee(newEmployee);

                    ServiceResponse.Success = true;
                    ServiceResponse.Data = new EmployeeDTO { 
                        Id = newEmployee.Id,
                        Username = createdUser.UserName ?? createdUser.Email,
                        Role = newEmployee.Role,
                    };

                    return ServiceResponse;
                }
     
                var errors = resultCreateIdentityUser.Errors.Select(e => e.Description)
                .Concat(resultAddClaimRole.Errors.Select(e => e.Description));

                ServiceResponse.Errors = errors;
                return ServiceResponse;
            }
            catch(Exception ex)
            {
                return new ServiceResponse<EmployeeDTO> {
                    Errors = new List<string> { ex.Message }
                };
            }
        }
    }
}
