using Microsoft.EntityFrameworkCore;
using Practica3_libros.Context;

var builder = WebApplication.CreateBuilder(args);

// Controllers
builder.Services.AddControllers();

// Entity Framework
builder.Services.AddDbContext<LibrosDbContext>(options =>
    options.UseSqlServer(
        builder.Configuration.GetConnectionString("bibliotecaDb")));

// Swagger
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

var app = builder.Build();

// Swagger solo en desarrollo (opcional)
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();