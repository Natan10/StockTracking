using Microsoft.EntityFrameworkCore;
using StockTracking.Data;
using StockTracking.Models;

namespace StockTracking.Repositories
{
    public interface IEmployeeRepository
    {
        public Task<Employee> GetEmployeeById(string id);
        public Task<List<Employee>> GetAll();

        public Task<Employee> CreateEmployee(Employee employee);
    }

    public class EmployeeRepository : IEmployeeRepository
    {
        private readonly DataContext _context;

        public EmployeeRepository(DataContext context)
        {
            _context = context;
        }

        public async Task<Employee> GetEmployeeById(string id) {
            var employee = await _context.Employees.FirstOrDefaultAsync(x => x.Id == id); 
            return employee is null ? throw new Exception("Funcionario nao encontrado.") : employee;
        }

        public async Task<List<Employee>> GetAll()
        {
            return await _context.Employees.ToListAsync();
        }

        public async Task<Employee> CreateEmployee(Employee employee)
        {
            var newEmployee = _context.Employees.Add(employee);
            await _context.SaveChangesAsync();

            return newEmployee.Entity;
        }
    }
}
