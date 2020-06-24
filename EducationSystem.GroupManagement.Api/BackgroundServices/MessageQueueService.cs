using System;
using System.Threading;
using System.Threading.Tasks;
using EducationSystem.GroupManagement.Api.Consumers;
using MassTransit;
using Microsoft.Extensions.Hosting;

namespace EducationSystem.GroupManagement.Api.BackgroundServices
{
    public class MessageQueueService : BackgroundService
    {
        private readonly IBusControl _bus;

        public MessageQueueService()
        {
            _bus = Bus.Factory.CreateUsingRabbitMq(cfg =>
            {
                cfg.Host(new Uri("rabbitmq://educationsystem.rabbitmq"));

                cfg.ReceiveEndpoint("test_queue", e =>
                {
                    e.Consumer<UpdateCustomerConsumer>();
                });
            });
        }

        protected override Task ExecuteAsync(CancellationToken stoppingToken)
        {
            return _bus.StartAsync();
        }

        public override Task StopAsync(CancellationToken cancellationToken)
        {
            return Task.WhenAll(base.StopAsync(cancellationToken), _bus.StopAsync());
        }
    }
}