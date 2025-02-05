using Microsoft.EntityFrameworkCore;
using Petshop.Application.Interfaces;
using Petshop.Application.Interfaces.Auth;
using Petshop.Application.Services;
using Petshop.Application.Services.Auth;
using Petshop.Domain.Interfaces;
using Petshop.Domain.Repository;
using Petshop.Infra.Data.Data;
using Petshop.Infra.Data.Repository;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
var connectionString = builder.Configuration.GetConnectionString("Database");

builder.Services.AddDbContext<ApplicationDbContext>(options =>
    options.UseSqlServer(connectionString));

builder.Services.AddControllers();


builder.Services.AddScoped<ISenhaService, SenhaService>();
builder.Services.AddScoped<IUsuarioAuthService, UsuarioAuthService>();
builder.Services.AddScoped<IUsuarioRepository, UsuarioRepository>();
builder.Services.AddScoped<IAnimalService, AnimalService>();
builder.Services.AddScoped<IAnimalRepository, AnimalRepository>();

//Swagger
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

var app = builder.Build();

app.UseSwagger();

app.UseSwaggerUI(options =>
{
    options.SwaggerEndpoint("/swagger/v1/swagger.json", "Petshop API V1");
});

app.UseAuthorization();

app.MapControllers();

app.Run();

