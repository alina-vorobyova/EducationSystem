using System;
using System.Collections.Generic;
using System.Text;
using MassTransit;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using RabbitMQ.Client;

namespace EducationSystem.StudentManagement.Infrastructure.Extensions
{
    public static class QueueExtension
    {
        public static IServiceCollection AddQueueServices(this IServiceCollection services/*, IConfiguration section*/)
        {
            //var appSettingsSection = section.GetSection("AppSettings");
            //var appSettings = appSettingsSection.Get<Appsettings>();
            //"HostName": "localhost"
            //"VirtualHost": "/"
            //"UserName": "guest"
            //"Password": "guest"

            services.AddSingleton(provider => Bus.Factory.CreateUsingRabbitMq(cfg =>
            {
                var host = cfg.Host("localhost", "/",
                    h => {
                        h.Username("guest");
                        h.Password("guest");
                    });

                cfg.ExchangeType = ExchangeType.Direct;
            }));

            services.AddSingleton<IPublishEndpoint>(provider => provider.GetRequiredService<IBusControl>());
            services.AddSingleton<ISendEndpointProvider>(provider => provider.GetRequiredService<IBusControl>());
            services.AddSingleton<IBus>(provider => provider.GetRequiredService<IBusControl>());

            return services;
        }
    }
}
