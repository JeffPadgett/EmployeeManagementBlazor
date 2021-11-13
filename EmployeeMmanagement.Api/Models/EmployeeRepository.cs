using EmployeeManagement.Models;
using Microsoft.EntityFrameworkCore;

namespace EmployeeMmanagement.Api.Models
{
    public class EmployeeRepository : IEmployeeRepository
    {
        private readonly AppDbContext _context;
        public EmployeeRepository(AppDbContext context)
        {
            _context = context;
        }

        public async Task<Employee> AddEmployee(Employee employee)
        {
            var result = await _context.Employees.AddAsync(employee);
            await _context.SaveChangesAsync();
            return result.Entity;
        }

        public async Task<Employee> DeleteEmployee(int employeeId)
        {
            var result = await _context.Employees.FirstOrDefaultAsync(emp => emp.EmployeeId == employeeId);
            if (result != null)
            {
                _context.Employees.Remove(result);
                await _context.SaveChangesAsync();
                return result;
            }
            throw new Exception("DeleteEmployee failed execution");
        }

        public async Task<Employee> GetEmployeeByEmail(string email)
        {
            return await _context.Employees.FirstOrDefaultAsync(emp => email == emp.Email);
        }

        public async Task<Employee> GetEmployeeById(int employeeId)
        {
            return await _context.Employees.FirstOrDefaultAsync(emp => emp.EmployeeId == employeeId);
        }

        public async Task<IEnumerable<Employee>> GetEmployees()
        {
            return await _context.Employees.ToListAsync();
        }

        public async Task<Employee> UpdateEmployee(Employee employee)
        {
            var result = await _context.Employees.FirstOrDefaultAsync(emp => emp.EmployeeId == employee.EmployeeId);

            if (result != null)
            {//can refactor with AutoMapper
                result.FirstName = employee.FirstName;
                result.LastName = employee.LastName;
                result.Email = employee.Email;
                result.DateOfBirth = employee.DateOfBirth;
                result.Gender = employee.Gender;
                result.DepartmentId = employee.DepartmentId;
                result.PhotoPath = employee.PhotoPath;

                await _context.SaveChangesAsync();

                return result;
            }

            throw new NullReferenceException("Could not find the correct employee to update");
        }
    }
}
