using EmployeeManagement.UI.Services;
using Microsoft.AspNetCore.Components;
using EmployeeManagement.Models;

namespace EmployeeManagement.UI.Pages
{
    public partial class EmployeeDetails
    {
        public Employee Employee { get; set; } = new Employee();

        [Parameter]
        public string Id { get; set; }

        [Inject]
        public IEmployeeService EmployeeService { get; set; }

        protected async override Task OnInitializedAsync()
        {
            Id = Id ?? "1";
            Employee = await EmployeeService.GetEmployeeById(int.Parse(Id));
        }
    }
}
