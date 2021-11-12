using EmployeeManagement.Models;
using Microsoft.AspNetCore.Components;

namespace EmployeeManagement.UI.Pages
{
    public class EmployeeListBase : ComponentBase
    {
        public IEnumerable<Employee> Employees { get; set; }

        protected override async Task OnInitializedAsync()
        {
            await Task.Run(LoadEmployees);
        }
        private void LoadEmployees()
        {
            System.Threading.Thread.Sleep(3000);
            Employee e1 = new Employee
            {
                EmployeeId = 1,
                FirstName = "John",
                LastName = "Hastings",
                Email = "john@pragimtech.com",
                DateOfBirth = new DateTime(1980, 10, 5),
                Gender = Gender.Male,
                DepartmentId = 1,
                PhotoPath = "images/John.jfif"
            };
            Employee e2 = new Employee
            {
                EmployeeId = 2,
                FirstName = "Ryan",
                LastName = "North",
                Email = "Ryan@pragimtech.com",
                DateOfBirth = new DateTime(1981, 10, 5),
                Gender = Gender.Male,
                DepartmentId = 2,
                PhotoPath = "images/Ryan.jfif"
            };
            Employee e3 = new Employee
            {
                EmployeeId = 3,
                FirstName = "Tyler",
                LastName = "Branscom",
                Email = "Tyler@pragimtech.com",
                DateOfBirth = new DateTime(1989, 10, 5),
                Gender = Gender.Male,
                DepartmentId = 3,
                PhotoPath = "images/Tyler.jfif"
            };
            Employee e4 = new Employee
            {
                EmployeeId = 1,
                FirstName = "Brandon",
                LastName = "Miller",
                Email = "Brandon@pragimtech.com",
                DateOfBirth = new DateTime(1990, 10, 5),
                Gender = Gender.Male,
                DepartmentId = 4,
                PhotoPath = "images/Brandon.jpg"
            };

            Employees = new List<Employee> { e1, e2, e3, e4 };
        }
    }
}
