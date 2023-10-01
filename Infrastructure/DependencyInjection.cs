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
            services.AddScoped<ICareerQuizRepository, CareerQuizRepository>();
            services.AddScoped<IQuizOptionRepository, QuizOptionRepository>();
            services.AddScoped<IQuizTypeRepository, QuizTypeRepository>();  
            services.AddScoped<IUserResponseRepository , UserResponseRepository>();
            services.AddScoped<ISuggestionRepository, SuggestionRepository>();
            services.AddScoped<IMajorRepository, MajorRepository>();
            services.AddScoped<IUnitOfWork,UnitOfWork>();
            services.AddDbContext<AppDbContext>(options =>
            {
                options.UseSqlServer(databaseConnection, sqlServerOptions =>
                {
                    // Enable retry on transient failures
                    sqlServerOptions.EnableRetryOnFailure(
                        maxRetryCount: 5,        // Number of retry attempts
                        maxRetryDelay: TimeSpan.FromSeconds(30), // Delay between retries
                        errorNumbersToAdd: null  // List of error codes to retry on (null means all transient errors)
                    );
                });

                // Enable sensitive data logging
                options.EnableSensitiveDataLogging();
            });
            return services;
        }
    }
}
