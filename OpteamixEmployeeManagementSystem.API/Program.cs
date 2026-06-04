using Microsoft.EntityFrameworkCore;
using OpteamixEmployeeManagementSystem.Data;
using OpteamixEmployeeManagementSystem.Data.Repository;
using OpteamixEmployeeManagementSystem.Domain.Repositories;

namespace OpteamixEmployeeManagementSystem.API
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);

            builder.Services.AddControllers();

            builder.Services.AddEndpointsApiExplorer();

            builder.Services.AddSwaggerGen();

            builder.Services.AddDbContext<EmployeeDbContext>(
                options =>
                options.UseSqlServer(
                    builder.Configuration.GetConnectionString(
                        "DefaultConnection")));

            builder.Services.AddScoped<
                IEmployeeRepository,
                EmployeeRepository>();

            var app = builder.Build();

            if (app.Environment.IsDevelopment())
            {
                app.UseSwagger();

                app.UseSwaggerUI();
            }

            app.UseHttpsRedirection();

            app.UseAuthorization();

            app.MapControllers();

            app.Run();
        }
    }
}