using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using OpteamixEmployeeManagementSystem.API.Middleware;
using OpteamixEmployeeManagementSystem.API.Services;
using System.Text;
using System.Text.Json.Serialization;
using Microsoft.AspNetCore.OData;
using OpteamixEmployeeManagementSystem.API.Profiles;




using OpteamixEmployeeManagementSystem.Data.Repository;
using OpteamixEmployeeManagementSystem.Domain.BusinessValidators;

namespace OpteamixEmployeeManagementSystem.API
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);

            // Controllers
            builder.Services.AddControllers()
                 .AddOData(options => options
                       .Select()
                       .Filter()
                       .OrderBy()
                       .Expand()
                       .Count()
                       .SetMaxTop(100))
                .AddJsonOptions(options =>
                {
                    options.JsonSerializerOptions.Converters
                        .Add(new JsonStringEnumConverter());
                });

            // Swagger
            builder.Services.AddEndpointsApiExplorer();
            builder.Services.AddSwaggerGen();

            // Employee Database Context
            builder.Services.AddDbContext<EmployeeDbContext>(options =>
                options.UseSqlServer(
                    builder.Configuration.GetConnectionString(
                        "DefaultConnection")));

            // Identity
            builder.Services.AddIdentity<
                ApplicationUser,
                IdentityRole>(options =>
                {
                    options.Password.RequiredLength = 8;
                    options.Password.RequireUppercase = true;
                    options.Password.RequireLowercase = true;
                    options.Password.RequireDigit = true;
                    options.Password.RequireNonAlphanumeric = true;
                })
                .AddEntityFrameworkStores<EmployeeDbContext>()
                .AddDefaultTokenProviders();

            builder.Services.ConfigureApplicationCookie(
                options =>
                {
                    options.Events.OnRedirectToLogin =
                        context =>
                        {
                            context.Response.StatusCode = 401;
                            return Task.CompletedTask;
                        };
                });

            // JWT Authentication
            builder.Services
                .AddAuthentication(options =>
                {
                    options.DefaultAuthenticateScheme =
                        JwtBearerDefaults.AuthenticationScheme;

                    options.DefaultChallengeScheme =
                        JwtBearerDefaults.AuthenticationScheme;

                    options.DefaultScheme =
                        JwtBearerDefaults.AuthenticationScheme;
                })
                .AddJwtBearer(options =>
                {
                    options.TokenValidationParameters =
                        new TokenValidationParameters
                        {
                            ValidateIssuer = true,
                            ValidateAudience = true,
                            ValidateLifetime = true,
                            ValidateIssuerSigningKey = true,

                            ValidIssuer =
                                builder.Configuration["Jwt:Issuer"],

                            ValidAudience =
                                builder.Configuration["Jwt:Audience"],

                            IssuerSigningKey =
                                new SymmetricSecurityKey(
                                    Encoding.UTF8.GetBytes(
                                        builder.Configuration["Jwt:Key"]!))
                        };
                });

            // Authorization
            builder.Services.AddAuthorization();

            // Services
            builder.Services.AddScoped<TokenServices>();

            builder.Services.Configure<EmailSettings>(
    builder.Configuration.GetSection("EmailSettings"));
            builder.Services.AddScoped<EmailService>();

            ////Memory Cache
            //builder.Services.AddMemoryCache();
            ////Response Cache
            //builder.Services.AddResponseCaching();
            // Output cache
            // Output Cache
            builder.Services.AddOutputCache();

            // Repositories
            builder.Services.AddScoped<
                IDepartmentRepository,
                DepartmentRepository>();

            builder.Services.AddScoped<
                IEmployeeRepository,
                EmployeeRepository>();

            builder.Services.AddScoped<
                IProjectRepository,
                ProjectRepository>();

            builder.Services.AddScoped<
                ITaskRepository,
                TaskRepository>();

            builder.Services.AddScoped<
                IReportRepository,
                ReportRepository>();
            // AutoMapper
            builder.Services.AddAutoMapper(
                typeof(MappingProfile));

            var app = builder.Build();

            // Swagger
            if (app.Environment.IsDevelopment())
            {
                app.UseSwagger();
                app.UseSwaggerUI();
            }

            // Middleware
            app.UseMiddleware<ExceptionMiddleware>();

            app.UseHttpsRedirection();

            //Middleware
            app.UseMiddleware<GlobalExceptionMiddleware>();

            app.UseAuthentication();

            app.UseAuthorization();

            app.MapControllers();

            app.Run();
        }
    }
}