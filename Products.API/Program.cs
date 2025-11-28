using System.Data;
using System.Data.SqlClient;
using System.Reflection;
using System.Text;
using Mapster;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.IdentityModel.Tokens;
using Microsoft.OpenApi.Models;
using Products.Application.Handlers.Categories;
using Products.Application.Handlers.Products;
using Products.Infrastructure.Implementations;
using Products.Infrastructure.Interfaces;
using Shared.Extensions;
using Shared.Helpers;

namespace Products.API;

public class Program
{
    public static void Main(string[] args)
    {
        var builder = WebApplication.CreateBuilder(args);

        // Add services to the container.

        builder.Services.AddControllers();
        
        // open cors
        builder.Services.AddCors(options =>
        {
            options.AddPolicy(
                "AllowAll", policy => policy
                    .AllowAnyOrigin()
                    .AllowAnyMethod()
                    .AllowAnyHeader());
        });
        
        
        // Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
        builder.Services.AddEndpointsApiExplorer();
        
        //basic services
        builder.Services.AddMapster();
        builder.Services.AddMediatR(cfg => cfg.RegisterServicesFromAssemblies(
            typeof(GetAllProductsQueryHandler).Assembly,
            typeof(GetSingleProductQueryHandler).Assembly,
            typeof(CreateProductCommandHandler).Assembly,
            typeof(UpdateProductCommandHandler).Assembly,
            typeof(DeleteProductCommandHandler).Assembly,
            typeof(GetAllCategoryQueryHandler).Assembly,
            typeof(GetProductsBySubCategoryIdQueryHandler).Assembly
            ));

        builder.Services.AddMemoryCache(options =>
        {
            options.CompactionPercentage = 0.2;
            options.ExpirationScanFrequency = TimeSpan.FromMinutes(5);
        });

        builder.Services.AddScoped<AuthHelper>();
        
        var assemblyName = Assembly.GetExecutingAssembly().GetName().Name;
        
        builder.Services.AddSerilogLogging(assemblyName ?? "Products");
        builder.Services.AddSingleton<ILogHelper, LogHelper>();
        
        var connectionString = builder.Configuration.GetConnectionString("Default");
        builder.Services.AddScoped<IDbConnection>(_ => new SqlConnection(connectionString));
        builder.Services.AddHttpContextAccessor();
        
        //Repo services
        builder.Services.AddScoped<IProductRepository, ProductRepository>();
        builder.Services.AddScoped<ICategoryRepository, CategoryRepository>();
        builder.Services.AddScoped<IStaticRepository, StaticRepository>();
        
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

        app.UseHttpsRedirection();
        
        app.UseAuthentication();
        app.UseAuthorization();


        app.MapControllers();

        app.Run();
    }
}