using System;
using System.IO;
using System.Reflection;
using AutoMapper;
using EducationSystem.StudentManagement.Application.Queries;
using EducationSystem.StudentManagement.Core;
using EducationSystem.StudentManagement.Infrastructure;
using EducationSystem.StudentManagement.Infrastructure.Extensions;
using MassTransit;
using MediatR;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.OpenApi.Models;

namespace EducationSystem.StudentManagement.Api
{
    public class Startup
    {
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public IConfiguration Configuration { get; }

        public void ConfigureServices(IServiceCollection services)
        {
            services.AddControllers();

            services.AddTransient<StudentsDbContext>(provider =>
            {
                var publishEndpoint = provider.GetService<IPublishEndpoint>();
                var optionsBuilder = new DbContextOptionsBuilder();
                var options = optionsBuilder.UseSqlServer(Configuration.GetConnectionString("Default")).Options;
                return new StudentsDbContext(options, publishEndpoint);
            });

            //services.AddDbContextPool<StudentsDbContext>(options =>
            //{
            //    options.UseSqlServer(Configuration.GetConnectionString("Default"));
            //});

            services.AddQueueServices();

            services.AddMediatR(typeof(GetStudentByIdQuery));
            services.AddAutoMapper(typeof(Startup));
            services.AddScoped<IStudentRepository, StudentRepository>();

            services.AddSwaggerGen(c =>
            {
                c.SwaggerDoc("v1", new OpenApiInfo { Title = "StudentManagement API", Version = "v1" });
                var xmlFile = $"{Assembly.GetExecutingAssembly().GetName().Name}.xml";
                var xmlPath = Path.Combine(AppContext.BaseDirectory, xmlFile);
                c.IncludeXmlComments(xmlPath);
            });
        }

        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }

            app.UseSwagger();

            app.UseSwaggerUI(c =>
            {
                c.SwaggerEndpoint("/swagger/v1/swagger.json", "My API V1");
            });

            app.UseHttpsRedirection();

            app.UseRouting();

            app.UseAuthorization();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
            });
        }
    }
}
