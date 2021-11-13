using EmployeeManagement.UI;
using EmployeeManagement.UI.Services;
using Microsoft.AspNetCore.Components.Web;
using Microsoft.AspNetCore.Components.WebAssembly.Hosting;

var builder = WebAssemblyHostBuilder.CreateDefault(args);
builder.RootComponents.Add<App>("#app");
builder.RootComponents.Add<HeadOutlet>("head::after");

builder.Services.AddHttpClient<IEmployeeService, EmployeeService>(client =>
{
    //client.BaseAddress = new Uri(builder.HostEnvironment.BaseAddress);
    client.BaseAddress = new Uri("https://localhost:7043/");
});

await builder.Build().RunAsync();
