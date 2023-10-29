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
        Task<ApiResponse<Employee>> RegisterEmployee(RegisterEmployeeDTO registerEmployee);
        Task<string> LoginEmployee(LoginEmployeeDTO loginEmployee);
    }

    public class AuthService : IAuthService
    {
        private readonly SignInManager<IdentityUser> _signInManager;
        private readonly UserManager<IdentityUser> _userManager;
        private readonly IEmployeeRepository _employeeRepository;

        public AuthService(
            SignInManager<IdentityUser> signInManager,
            UserManager<IdentityUser> userManager,
            IEmployeeRepository employeeRepository
        )
        {
            _signInManager = signInManager;
            _userManager = userManager;
            _employeeRepository = employeeRepository;
        }

        public Task<string> LoginEmployee(LoginEmployeeDTO loginEmployee)
        {
            throw new NotImplementedException();
        }

        public async Task<ApiResponse<Employee>> RegisterEmployee(RegisterEmployeeDTO registerEmployee)
        {
            try
            {
                var apiResponse = new ApiResponse<Employee>();

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

                    apiResponse.Success = true;
                    apiResponse.Data = createdEmployee;

                    return apiResponse;
                }
     
                var errors = resultCreateIdentityUser.Errors.Select(e => e.Description)
                .Concat(resultAddClaimRole.Errors.Select(e => e.Description));

                apiResponse.Errors = errors;
                return apiResponse;
            }
            catch(Exception ex)
            {
                return new ApiResponse<Employee> {
                    Errors = new List<string> { ex.Message }
                };
            }
        }
    }
}
