using BlazorBiblioteca.Client.Pages;
using BlazorBiblioteca.Components;
using BlazorBiblioteca.Context;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddRazorComponents()
    .AddInteractiveWebAssemblyComponents();

//add servicios: Controller y HttpClient
builder.Services.AddControllers();
builder.Services.AddHttpClient();

//add servicio Bootstrap
builder.Services.AddBlazorBootstrap();

//add servicios: DbContext
builder.Services.AddDbContext<LibroDBContext>(options =>
{
    options.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection"));
}
);

var app = builder.Build();




// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseWebAssemblyDebugging();
}
else
{
    app.UseExceptionHandler("/Error", createScopeForErrors: true);
    // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
    app.UseHsts();
}




app.UseHttpsRedirection();

app.UseStaticFiles();
app.UseAntiforgery();



app.MapRazorComponents<App>()
    .AddInteractiveWebAssemblyRenderMode()
    .AddAdditionalAssemblies(typeof(BlazorBiblioteca.Client._Imports).Assembly);

//Add Mapeo del Controlador
app.MapControllers();

app.Run();
