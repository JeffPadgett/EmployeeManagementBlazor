using EmployeeManagement.Models;
using EmployeeManagement.UI.Services;
using Microsoft.AspNetCore.Components;

namespace EmployeeManagement.UI.Pages
{
    public class EmployeeListBase : ComponentBase
    {
        public IEnumerable<Employee> Employees { get; set; }

        [Inject]
        public IEmployeeService EmployeeService { get; set; }

        protected override async Task OnInitializedAsync()
        {
           Employees = await EmployeeService.GetEmployees();
        }
    }
}
