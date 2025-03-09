using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Petshop.Application.Interfaces;
using Petshop.Application.Interfaces.Auth;
using Petshop.Application.Services;
using Petshop.Application.Services.Auth;
using Petshop.Domain.Interfaces;
using Petshop.Domain.Repository;
using Petshop.Infra.Data.Data;
using Petshop.Infra.Data.Repository;

namespace Petshop.Infra.Ioc
{
    public static class DependencyInjection
    {
        public static IServiceCollection AddInfrastructure(this IServiceCollection services, IConfiguration configuration)
        {
            services.AddDbContext<ApplicationDbContext>(options =>
            {
                options.UseSqlServer(configuration.GetConnectionString("Database"),
                b => b.MigrationsAssembly(typeof(ApplicationDbContext).Assembly.FullName));
            });

            services.AddScoped<IClienteRepository, ClienteRepository>();
            //services.AddScoped<ISenhaService, SenhaService>();
            services.AddScoped<IClienteService, ClienteService>();
            services.AddScoped<IAnimalService, AnimalService>();
            services.AddScoped<IAnimalRepository, AnimalRepository>();
            services.AddScoped<IConsultaService, ConsultaService>();
            services.AddScoped<IConsultaRepository, ConsultaRepository>();


            return services;
        }
    }
}