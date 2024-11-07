using Digitalers.DataAccessLayer.Contracts;
using Digitalers.DataAccessLayer.Models;
using Digitalers.DataAccessLayer.Repositories;
using Digitalers.DomainLayer.Entities;
using Digitalers.Shared.Dtos;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);



// Add services to the container.

builder.Services.AddDbContext<AppDbContext>(options => 
    options.UseMySql(
    builder.Configuration.GetConnectionString("defaultConnection"), new MySqlServerVersion(new Version(8, 0, 21))
    )
    );

builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();


//builder.Services.AddScoped(typeof(IRepository<>), typeof(GenericRepository<>));
builder.Services.AddScoped<IRepository<CursoDto>, CursoRepository>();
builder.Services.AddScoped<IRepository<ProfesorDto>, ProfesorRepository>();
builder.Services.AddScoped<IPerson<ProfesorDto>, ProfesorRepository>();
builder.Services.AddScoped<IRepository<EstudianteDto>, EstudianteRepository>();
builder.Services.AddScoped<IPerson<EstudianteDto>,EstudianteRepository>();


builder.Services.AddCors(options =>
{
    options.AddPolicy("frontedMVC",
        builder =>
        {
            builder.AllowAnyOrigin()
                   .AllowAnyMethod()
                   .AllowAnyHeader();
        });
});



var app = builder.Build();


app.UseCors("frontedMVC");


app.UseRouting();

app.UseEndpoints(endpoints =>
{
    endpoints.MapControllers();
});

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}



app.Run();

