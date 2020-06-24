using System;
using System.Linq;
using System.Threading.Tasks;
using EducationSystem.Common.Utils;
using EducationSystem.StudentManagement.Core;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace EducationSystem.StudentManagement.Infrastructure.Extensions
{
    public static class InfrastructureExtensions
    {
        public static void AddPersistence(this IServiceCollection services, IConfiguration config)
        {
            services.AddEntityFrameworkSqlServer();

            services.AddDbContextPool<StudentsDbContext>((serviceProvider, options) =>
            {
                options.UseSqlServer(config.GetConnectionString("Default"), sqlServerOptions => 
                    sqlServerOptions.EnableRetryOnFailure());
                options.UseInternalServiceProvider(serviceProvider);
            });

            services.AddScoped<IStudentRepository, StudentRepository>();
        }

        public static async Task<PaginatedList<T>> ToPaginatedListAsync<T>(this IQueryable<T> source, int pageIndex, int pageSize)
        {
            var count = await source.CountAsync();
            var items = await source.Skip((pageIndex - 1) * pageSize).Take(pageSize).ToListAsync();
            return new PaginatedList<T>(items, count, pageIndex, pageSize);
        }
    }
}
