using EmployeeManagement.UI.Services;
using Microsoft.AspNetCore.Components;
using EmployeeManagement.Models;
using Microsoft.AspNetCore.Components.Web;

namespace EmployeeManagement.UI.Pages
{
    public partial class EmployeeDetails
    {
        public Employee Employee { get; set; } = new Employee();

        protected string Coordinates { get; set; }

        [Parameter]
        public string Id { get; set; }

        [Inject]
        public IEmployeeService EmployeeService { get; set; }

        protected async override Task OnInitializedAsync()
        {
            Id = Id ?? "1";
            Employee = await EmployeeService.GetEmployeeById(int.Parse(Id));
        }

        protected void Mouse_Move(MouseEventArgs e)
        {
            Coordinates = $"X = {e.ClientX} Y = {e.ClientY}";
        }
    }
}
