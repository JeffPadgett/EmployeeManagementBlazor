using EmployeeMmanagement.Api.Models;
using Microsoft.EntityFrameworkCore;

namespace EmployeeMmanagement.Api.Extensions
{
    internal static class ServiceConfig
    {
        public static void ConfigureDbContextService(this IServiceCollection services, IConfiguration config)
        {
            services.AddDbContext<AppDbContext>(options => options.UseSqlServer(config.GetConnectionString("DBConnection")));
            services.AddScoped<IDepartmentRepository, DepartmentRepository>();
            services.AddScoped<IEmployeeRepository, EmployeeRepository>();
        }
    }
}
