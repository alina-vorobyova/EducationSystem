using System;
using System.IO;
using System.Linq;
using System.Reflection;
using AutoMapper;
using EducationSystem.Common.ApiUtils;
using EducationSystem.StudentManagement.Application.Profiles;
using EducationSystem.StudentManagement.Application.Queries;
using EducationSystem.StudentManagement.Core;
using EducationSystem.StudentManagement.Infrastructure;
using EducationSystem.StudentManagement.Infrastructure.Extensions;
using FluentValidation.AspNetCore;
using MassTransit;
using MediatR;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
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
            services.AddControllers()
                .AddFluentValidation(options => options.RegisterValidatorsFromAssembly(Assembly.GetExecutingAssembly()))
                .ConfigureApiBehaviorOptions(options =>
                    options.InvalidModelStateResponseFactory = actionContext => 
                        new BadRequestObjectResult(new ApiResult(actionContext.ModelState)));
                
            services.AddPersistence(Configuration);
            services.AddRabbitMqBus(Configuration);
            services.AddMediatR(typeof(GetStudentByIdQuery));
            services.AddAutoMapper(typeof(AutoMapperProfile));
            services.AddSwagger("StudentManagement API", 1);
        }

        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }

            app.UseSwagger();
            app.UseSwaggerUI(c => c.SwaggerEndpoint("/swagger/v1/swagger.json", "My API V1"));
            app.UseHttpsRedirection();
            app.UseRouting();
            app.UseAuthorization();
            app.UseEndpoints(endpoints => endpoints.MapControllers());
        }
    }
}
