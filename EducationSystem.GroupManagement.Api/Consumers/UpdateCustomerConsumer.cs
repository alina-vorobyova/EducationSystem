using System;
using System.Threading.Tasks;
using EducationSystem.Common.Contracts;
using MassTransit;

namespace EducationSystem.GroupManagement.Api.Consumers
{
    public class UpdateCustomerConsumer : IConsumer<StudentExposedEvent>
    {
        public Task Consume(ConsumeContext<StudentExposedEvent> context)
        {
            Console.WriteLine("Received!!!");
            Console.WriteLine(context.Message.StudentId);
            return Task.CompletedTask;
        }
    }
}