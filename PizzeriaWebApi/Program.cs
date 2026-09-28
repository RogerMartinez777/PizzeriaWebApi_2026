// 1. Agregar todos los using:
using Microsoft.EntityFrameworkCore;
using PizzeriaRepository.Data;
using PizzeriaRepository.Data.Implementations;
using PizzeriaRepository.Data.Interfaces;
using PizzeriaRepository.Services;

// Inyección de Dependencias: Se configura en Program.cs mediante builder.Services.AddDbContext<ApplicationDbContext>(...) leyendo la cadena de conexión desde appsettings.json

var builder = WebApplication.CreateBuilder(args);

// 2. Obtener la cadena de conexión desde appsettings.json
var connectionString = builder.Configuration.GetConnectionString("DefaultConnection");

// 3. Registrar DbContext con SQL Server
builder.Services.AddDbContext<PizzeriaDbContext>(options  =>
    options.UseSqlServer(connectionString));

// 4. Registrar el Repositorio para inyección de Depencencias
// Le indica al Framework que cada vez que un controlador pida IPizzaRepository le entregue una instancia de PizzaRepository
builder.Services.AddScoped<IPizzaRepository, PizzaRepository>();

// 5. Servicio directo como clase concreta
builder.Services.AddScoped<PizzaService>();

// Add services to the container.

builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();
