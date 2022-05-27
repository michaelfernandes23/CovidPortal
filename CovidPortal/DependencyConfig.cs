using CovidPortal.Domain.AutoMapper;
using CovidPortal.Services;
using CovidPortal.Services.Interfaces;
using CovidPortal.SQL.Infrastructure.Data;
using CovidPortal.SQL.Infrastructure.Interfaces;
using CovidPortal.SQL.Infrastructure.Repositories;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Http;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.IdentityModel.Tokens;
using Microsoft.OpenApi.Models;
using Serilog;
using System;
using System.Data;
using System.Linq;
using System.Text;

namespace CovidPortal.API
{
    public static class DependencyConfig
    {
        public static void AddRepositories(this IServiceCollection services)
        {
            services.AddScoped(typeof(ICovidCountryDetailSqlRepository), typeof(CovidCountryDetailSqlRepository));
        }

        public static void AddDataServices(this IServiceCollection services)
        {
            services.AddSingleton<IHttpContextAccessor, HttpContextAccessor>();

            services.AddScoped(typeof(IUnitOfWork), typeof(UnitOfWork));
            services.AddScoped<ICovidService, CovidService>();
        }

        public static void AddDbContext(this IServiceCollection services, IConfiguration configuration, IHostEnvironment env)
        {
            var connectionString = configuration.GetConnectionString("DB");
            services.AddDbContext<ServiceDbContext>(options =>
            {
                options.EnableSensitiveDataLogging();
                options.UseSqlServer(connectionString, sqlServerOptions => sqlServerOptions.CommandTimeout(90));
            });
        }

        public static void AddSwagger(this IServiceCollection services)
        {
            services.AddSwaggerGen(c =>
            {
                c.SwaggerDoc("v1",
                             new OpenApiInfo
                             {
                                 Version = "1.0",
                                 Title = "Covid Portal API",
                                 Description = "Summary of Covid cases in different countries",
                             });

                c.AddSecurityDefinition("Bearer",
                                        new OpenApiSecurityScheme
                                        {
                                            In = ParameterLocation.Header,
                                            Description = "Please enter into field the word 'Bearer' following by space and JWT",
                                            Name = "Authorization",
                                            Type = SecuritySchemeType.ApiKey
                                        });

                c.AddSecurityRequirement(new OpenApiSecurityRequirement
                {
                    {
                        new OpenApiSecurityScheme
                        {
                            In = ParameterLocation.Header,
                            Description = "Please enter into field the word 'Bearer' following by space and JWT",
                            Name = "Authorization",
                            Type = SecuritySchemeType.ApiKey
                        },
                        Enumerable.Empty<string>().ToList()
                    },
                });
            });
        }

        public static void AddAuth(this IServiceCollection services, IConfiguration configuration)
        {
            var audienceConfig = configuration.GetSection("Audience");
            var signingKey = new SymmetricSecurityKey(Encoding.ASCII.GetBytes(audienceConfig["Secret"]));
            var tokenValidationParameters = new TokenValidationParameters
            {
                NameClaimType = "http://schemas.xmlsoap.org/ws/2005/05/identity/claims/nameidentifier",
                ValidateIssuerSigningKey = true,
                IssuerSigningKey = signingKey,
                ValidateIssuer = true,
                ValidIssuer = audienceConfig["Iss"],
                ValidateAudience = true,
                ValidAudience = audienceConfig["Aud"],
                ValidateLifetime = true,
                ClockSkew = TimeSpan.Zero,
                RequireExpirationTime = true
            };

            services.AddAuthentication(options =>
            {
                options.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
                options.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
            })
                    .AddJwtBearer(x =>
                    {
                        x.RequireHttpsMetadata = false;
                        x.TokenValidationParameters = tokenValidationParameters;
                    });
        }

        public static void AddLogs(this IServiceCollection services, IConfiguration configurations)
        {
            Log.Logger = new LoggerConfiguration()
                         .ReadFrom.Configuration(configurations)
                         .CreateLogger();
        }

        public static void AddMapper(this IServiceCollection services)
        {
            services.AddAutoMapper(typeof(Mappings));
        }

        public static void AddCoresAllowAll(this IServiceCollection services)
        {
            services.AddCors(c =>
            {
                c.AddPolicy("AllowAllOrigin",
                            options => options.AllowAnyOrigin()
                                              .AllowAnyMethod()
                                              .AllowAnyHeader());
            });
        }
    }
}
