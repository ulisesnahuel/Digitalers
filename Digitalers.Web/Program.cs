using Digitalers.Web.Controllers;
using Digitalers.Web.Services.Contracts;
using Digitalers.Web.Services.Implementation;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddControllersWithViews();

builder.Services.AddHttpClient<IEstudianteService, EstudianteService>();
builder.Services.AddHttpClient<IProfesorService, ProfesorService>();
builder.Services.AddHttpClient<ICursoService, CursoService>();

var app = builder.Build();




if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Home/Error");
}
app.UseStaticFiles();

app.UseRouting();

app.UseAuthorization();

app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Home}/{action=Index}/{id?}");

app.Run();
