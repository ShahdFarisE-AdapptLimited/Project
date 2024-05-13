using Client;
using Client.Models;
using Client.Services;
using Microsoft.AspNetCore.Components.Web;
using Microsoft.AspNetCore.Components.WebAssembly.Hosting;
using static System.Net.Http.HttpClient;


var builder = WebAssemblyHostBuilder.CreateDefault(args);
builder.RootComponents.Add<App>("#app");
builder.RootComponents.Add<HeadOutlet>("head::after");


builder.Services.AddHttpClient<IEmployeeService, EmployeeService>(
    client =>
    {
        client.BaseAddress = new Uri(@"https://localhost:7263/api/");
    });

builder.Services.AddHttpClient<IDepartmentService, DepartmentService>(
    client=>
    {
        client.BaseAddress = new Uri(@"https://localhost:7263/api/");
    });
builder.Services.AddScoped(sp => new HttpClient
{
    BaseAddress = new Uri(builder.HostEnvironment.BaseAddress)
});

await builder.Build().RunAsync();
