using Application;
using Application.Common;
using Application.InterfaceRepository;
using Application.InterfaceService;
using Application.Service;
using Infrastructure.Repository;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Configuration;
using Microsoft.Extensions.Configuration;

namespace Infrastructure
{
    public static class DependencyInjection
    {
        public static IServiceCollection AddInfrastructuresService(this IServiceCollection services, string databaseConnection)
        {
            
            services.AddSingleton<ICurrentTime, CurrentTime>();
            services.AddScoped<IUserRepository, UserRepository>();
            services.AddScoped<IUnitOfWork,UnitOfWork>();   
            services.AddDbContext<AppDbContext>(option => option.UseSqlServer(databaseConnection).EnableSensitiveDataLogging());
            return services;
        }
    }
}
