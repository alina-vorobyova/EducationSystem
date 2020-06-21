using EducationSystem.StudentManagement.Core;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace EducationSystem.StudentManagement.Infrastructure.Extensions
{
    public static class InfrastructureStartupExtensions
    {
        public static void AddPersistence(this IServiceCollection services, IConfiguration config)
        {
            services.AddDbContext<StudentsDbContext>(options =>
            {
                options.UseSqlServer(config.GetConnectionString("Default"));
            });

            services.AddScoped<IStudentRepository, StudentRepository>();
        }
    }
}
