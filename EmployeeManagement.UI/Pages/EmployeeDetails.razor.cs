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

        private string ButtonText { get; set; } = "Hide Footer";
        private string CssClass { get; set; } = null;

        [Parameter]
        public string Id { get; set; }

        [Inject]
        public IEmployeeService EmployeeService { get; set; }

        protected async override Task OnInitializedAsync()
        {
            Id = Id ?? "1";
            Employee = await EmployeeService.GetEmployeeById(int.Parse(Id));
        }

        protected void MouseMoveEventHandler(MouseEventArgs e)
        {
            Coordinates = $"X = {e.ClientX} Y = {e.ClientY}";
        }

        protected void ButtonClickEventHandler()
        {
            if (ButtonText == "Hide Footer")
            {
                ButtonText = "Show Footer";
                CssClass = "HideFooter";
            }
            else
            {
                CssClass = null;
                ButtonText = "Hide Footer";
            }
        }
    }
}
