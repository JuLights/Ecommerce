using System.Data;
using System.Data.SqlClient;
using System.Reflection;
using System.Text;
using Authorization.Application.Handlers;
using Authorization.Infrastructure;
using Authorization.Infrastructure.Implementations;
using Authorization.Infrastructure.Interfaces;
using Mapster;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.IdentityModel.Tokens;
using Microsoft.OpenApi.Models;
using Serilog;
using Shared.Extensions;
using Shared.Helpers;
using Shared.Middlewares;

namespace Authorization.API;

public class Program
{
    public static void Main(string[] args)
    {
        var builder = WebApplication.CreateBuilder(args);

        // Add services to the container.
        builder.Services.AddAuthorization();
        
        // open cors
        builder.Services.AddCors(options =>
        {
            options.AddPolicy(
                "AllowAll", policy => policy
                    .AllowAnyOrigin()
                    .AllowAnyMethod()
                    .AllowAnyHeader());
        });
        
        builder.Services.AddControllers();
        // Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
        builder.Services.AddEndpointsApiExplorer();
        builder.Services.AddSwaggerGen();
        
        
        
        builder.Services.AddMapster();
        builder.Services.AddMediatR(cfg => cfg.RegisterServicesFromAssemblies(
            typeof(SignInQueryHandler).Assembly));
        
        //test logger from Shared
        var assemblyName = Assembly.GetExecutingAssembly().GetName().Name;
        builder.Services.AddSerilogLogging(assemblyName ?? "Authorization");
        
        
        builder.Services.AddSingleton<ILogHelper, LogHelper>();
        builder.Services.AddSingleton<AuthHelper>();
        
        builder.Services.AddSingleton<IAuthRepository, AuthRepository>();
        builder.Services.AddScoped<IUserRepository, UserRepository>();
        
        builder.Services.AddSingleton<TokenService>();
        builder.Services.AddHttpContextAccessor();
        var connectionString = builder.Configuration.GetConnectionString("Default");
        builder.Services.AddSingleton<IDbConnection>(_ => new SqlConnection(connectionString));

        #region Auth
        builder.Services.AddSwaggerGen(options =>
        {
            options.AddSecurityDefinition("Bearer", new OpenApiSecurityScheme
            {
                Name = "Authorization",
                In = ParameterLocation.Header,
                Type = SecuritySchemeType.Http,
                Scheme = "Bearer"
            });

            options.AddSecurityRequirement(new OpenApiSecurityRequirement
            {
                {
                    new OpenApiSecurityScheme
                    {
                        Reference = new OpenApiReference
                        {
                            Type = ReferenceType.SecurityScheme,
                            Id = "Bearer"
                        }
                    },
                    Array.Empty<string>()
                }
            });
        });
        
        var keyString = builder.Configuration["Jwt:Key"] ?? "";
        var key = Encoding.ASCII.GetBytes(keyString);

        builder.Services.AddAuthentication(x =>
        {
            x.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
            x.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
        }).AddJwtBearer(x =>
        {
            x.RequireHttpsMetadata = false;
            x.SaveToken = true;
            x.TokenValidationParameters = new TokenValidationParameters
            {
                ValidateIssuerSigningKey = true,
                IssuerSigningKey = new SymmetricSecurityKey(key),
                ValidateIssuer = true,
                ValidateAudience = true,
                ValidIssuer = builder.Configuration["Jwt:Issuer"],
                ValidAudience = builder.Configuration["Jwt:Audience"]
            };
        });

        #endregion

        var app = builder.Build();

        // Configure the HTTP request pipeline.
        if (app.Environment.IsDevelopment())
        {
            app.UseSwagger();
            app.UseSwaggerUI();
        }
        
        app.UseCors("AllowAll");

        app.UseMiddleware<ExceptionHandlingMiddleware>();
        app.UseHttpsRedirection();
        app.UseAuthentication();
        app.UseAuthorization();
        app.MapControllers();
        app.Run();
    }
}