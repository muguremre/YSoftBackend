using Microsoft.EntityFrameworkCore;
using HRSystem.Business;
using HRSystem.Data.Repositories;
using HRSystem.Data;

namespace YSOFTBackend
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);
            builder.Services.AddControllers().AddJsonOptions(options =>
            {
                options.JsonSerializerOptions.ReferenceHandler = System.Text.Json.Serialization.ReferenceHandler.Preserve;
            });

            // Add services to the container.

            // Veritabaný baðlantýsýný ekle
            builder.Services.AddDbContext<HRSystemDbContext>(options =>
                options.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection")));

            // Repository'leri Dependency Injection için ekle
            builder.Services.AddScoped<IUserRepository, UserRepository>();
            builder.Services.AddScoped<IEmployeeRepository, EmployeeRepository>();
            builder.Services.AddScoped<IProjectRepository, ProjectRepository>();

            // Servis katmanlarýný Dependency Injection için ekle
            builder.Services.AddScoped<UserService>();
            builder.Services.AddScoped<EmployeeService>();
            builder.Services.AddScoped<ProjectService>();

            // Controller'larý ekle
            builder.Services.AddControllers();

            // Swagger/OpenAPI yapýlandýrmasý
            builder.Services.AddEndpointsApiExplorer();
            builder.Services.AddSwaggerGen();

            var app = builder.Build();

            // Configure the HTTP request pipeline.
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
