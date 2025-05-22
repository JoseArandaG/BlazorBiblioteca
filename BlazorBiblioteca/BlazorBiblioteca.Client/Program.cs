using Microsoft.AspNetCore.Components.WebAssembly.Hosting;
using Microsoft.Extensions.DependencyInjection;

var builder = WebAssemblyHostBuilder.CreateDefault(args);

//Agregar Servicio Http
builder.Services.AddScoped(sp => 
new HttpClient
{
    BaseAddress = new Uri(builder.Configuration["FrontendUrl"] ?? "https://localhost:7251")
});


//add servicio Bootstrap
builder.Services.AddBlazorBootstrap();

await builder.Build().RunAsync();
