using System;
using System.IO;
using System.Reflection;
using MassTransit;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.OpenApi.Models;

namespace EducationSystem.Common.ApiUtils
{
    public static class ServiceProviderExtensions
    {
        public static void AddSwagger(this IServiceCollection services, string title, int version)
        {
            services.AddSwaggerGen(c =>
            {
                c.SwaggerDoc($"v{version}", new OpenApiInfo { Title = title, Version = $"v{version}" });
                var xmlFile = $"{Assembly.GetEntryAssembly()?.GetName().Name}.xml";
                var xmlPath = Path.Combine(AppContext.BaseDirectory, xmlFile);
                c.IncludeXmlComments(xmlPath);
                c.CustomSchemaIds(x => x.FullName);
            });
        }

        public static void AddRabbitMqBus(this IServiceCollection services, IConfiguration config)
        {
            var host = config["RabbitMQ:HostUri"];
            var bus = Bus.Factory.CreateUsingRabbitMq(cfg =>
            {
                cfg.Host(new Uri(host));
            });
            bus.Start();
            services.AddSingleton<IBus>(bus);
        }
    }
}
