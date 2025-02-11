using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using Microsoft.OpenApi.Models;
using Petshop.Application.Interfaces;
using Petshop.Application.Interfaces.Auth;
using Petshop.Application.Services;
using Petshop.Application.Services.Auth;
using Petshop.Domain.Interfaces;
using Petshop.Domain.Repository;
using Petshop.Infra.Data.Data;
using Petshop.Infra.Data.Repository;
using Swashbuckle.AspNetCore.Filters;
using System.Text;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
var connectionString = builder.Configuration.GetConnectionString("Database");

builder.Services.AddDbContext<ApplicationDbContext>(options =>
    options.UseSqlServer(connectionString));

builder.Services.AddControllers();

builder.Services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme).AddJwtBearer(options =>
{
    options.TokenValidationParameters = new TokenValidationParameters
    {
        ValidateIssuerSigningKey = true,
        IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(builder.Configuration.GetSection("AppSettings:Token").Value)),
        ValidateAudience = false,
        ValidateIssuer = false
    };
});

builder.Services.AddScoped<ISenhaService, SenhaService>();
builder.Services.AddScoped<IUsuarioAuthService, UsuarioAuthService>();
builder.Services.AddScoped<IUsuarioRepository, UsuarioRepository>();
builder.Services.AddScoped<IAnimalService, AnimalService>();
builder.Services.AddScoped<IAnimalRepository, AnimalRepository>();

builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen(options =>
{
    options.AddSecurityDefinition("oauth2", new OpenApiSecurityScheme
    {
        Description = "Insira o token JWT desta forma: Bearer {seu_token}",
        In = ParameterLocation.Header,
        Name = "Authorization",
        Type = SecuritySchemeType.ApiKey
    });
    options.OperationFilter<SecurityRequirementsOperationFilter>();
});

var app = builder.Build();

app.UseSwagger();
app.UseSwaggerUI(options =>
{
    options.SwaggerEndpoint("/swagger/v1/swagger.json", "Petshop API V1");
});

app.UseAuthentication();
app.UseAuthorization();

app.MapControllers();

app.Run();
