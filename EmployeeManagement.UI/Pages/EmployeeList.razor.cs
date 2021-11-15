using EmployeeManagement.Models;
using EmployeeManagement.UI.Services;
using Microsoft.AspNetCore.Components;

namespace EmployeeManagement.UI.Pages
{
    public partial class EmployeeList
    {

        [Inject]
        public IEmployeeService EmployeeService { get; set; }
        public bool ShowFooter { get; set; } = true;

        public IEnumerable<Employee> Employees { get; set; }

        protected override async Task OnInitializedAsync()
        {
            Employees = (await EmployeeService.GetEmployees()).ToList();
        }

        public int SelectedEmployeesCount { get; set; } = 0;
        protected void SelectionChangedEventHandler(bool isSelected)
        {
            if (isSelected)
            {
                SelectedEmployeesCount++;
            }
            else
            {
                SelectedEmployeesCount--;
            }
        }
    }
}