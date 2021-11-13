using EmployeeManagement.Models;
using System.Net.Http.Json;

namespace EmployeeManagement.UI.Services
{
    public class EmployeeService : IEmployeeService
    {
        private readonly HttpClient _httpClient;

        public EmployeeService(IHttpClientFactory httpClient)
        {
            _httpClient = httpClient.CreateClient("EmployeeServiceClient");
        }

        public async Task<IEnumerable<Employee>> GetEmployees()
        {
            return await _httpClient.GetFromJsonAsync<Employee[]>("api/employees");
        }

        public Task<Employee> AddEmployee(Employee employee)
        {
            throw new NotImplementedException();
        }

        public Task<Employee> DeleteEmployee(int employeeId)
        {
            throw new NotImplementedException();
        }

        public Task<Employee> GetEmployeeByEmail(string email)
        {
            throw new NotImplementedException();
        }

        public Task<Employee> GetEmployeeById(int employeeId)
        {
            throw new NotImplementedException();
        }

        public Task<IEnumerable<Employee>> Search(string name, Gender? gender)
        {
            throw new NotImplementedException();
        }

        public Task<Employee> UpdateEmployee(Employee employee)
        {
            throw new NotImplementedException();
        }
    }
}
