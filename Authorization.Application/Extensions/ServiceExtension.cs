using Authorization.Application.Handlers;
using Authorization.Infrastructure;
using Authorization.Infrastructure.Implementations;
using Authorization.Infrastructure.Interfaces;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Shared.Extensions;
using Shared.Helpers;
using Shared.Models;
using System.Reflection;

namespace Authorization.Application.Extensions
{
    public static class ServiceExtension
    {
        //debug services
        public static IServiceCollection AddDebugServices(this IServiceCollection services, IConfiguration config)
        {
            var assemblyName = Assembly.GetExecutingAssembly().GetName().Name;

            // Logging & Config
            services.AddSerilogLogging(assemblyName ?? "Authorization");
            services.Configure<AppSettings>(config.GetSection("AppSettings"));

            // Helpers - Singletons are fine if they don't use the Database
            services.AddSingleton<ILogHelper, LogHelper>();
            services.AddSingleton<AuthHelper>();

            // CHANGED TO SCOPED: Repositories and TokenServices 
            // almost always need a Scoped lifetime to work with DB/Auth
            services.AddScoped<IAuthRepository, AuthRepository>();
            services.AddScoped<IUserRepository, UserRepository>();
            services.AddScoped<TokenService>();

            // Infrastructure
            services.AddHttpContextAccessor();

            // IMPORTANT: Ensure MediatR is registered here or in Program.cs
            // You must point it to the assembly where your Handlers live
            services.AddMediatR(cfg => cfg.RegisterServicesFromAssembly(typeof(SignInQueryHandler).Assembly));

            return services;
        }

        public static IServiceCollection AddReleaseServices(this IServiceCollection services, IConfiguration config)
        {
            var assemblyName = Assembly.GetExecutingAssembly().GetName().Name;

            services.AddSerilogLogging(assemblyName ?? "Authorization");
            services.Configure<AppSettings>(config.GetSection("AppSettings"));
            services.AddSingleton<ILogHelper, LogHelper>();
            services.AddSingleton<AuthHelper>();
            services.AddSingleton<IAuthRepository, AuthRepository>();
            services.AddScoped<IUserRepository, UserRepository>();
            services.AddSingleton<TokenService>();
            services.AddHttpContextAccessor();


            return services;
        }


    }
}
