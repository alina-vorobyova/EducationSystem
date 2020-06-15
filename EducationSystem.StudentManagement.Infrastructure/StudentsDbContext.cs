using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using EducationSystem.Common.Abstractions;
using EducationSystem.StudentManagement.Core;
using EducationSystem.StudentManagement.Core.DomainEvents;
using MassTransit;
using Microsoft.EntityFrameworkCore;
using Newtonsoft.Json;

namespace EducationSystem.StudentManagement.Infrastructure
{
    public class StudentsDbContext : DbContext
    {
        private readonly IPublishEndpoint _publishEndpoint;

        public StudentsDbContext(DbContextOptions options, IPublishEndpoint publishEndpoint) : base(options)
        {
            _publishEndpoint = publishEndpoint;
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            modelBuilder.Entity<Student>()
                .OwnsOne(x => x.FullName, x =>
                {
                    x.Property(y => y.FirstName).IsRequired().HasColumnType("NVARCHAR(100)").HasColumnName("FirstName");
                    x.Property(y => y.LastName).IsRequired().HasColumnType("NVARCHAR(100)").HasColumnName("LastName");
                    x.Property(y => y.MiddleName).IsRequired(false).HasColumnType("NVARCHAR(100)").HasColumnName("MiddleName");
                })
                .OwnsMany(x => x.Phones, x =>
                {
                    x.Property(y => y.Number).IsRequired().HasColumnType("NVARCHAR(100)").HasColumnName("Number");
                    x.Property(y => y.Type).IsRequired().HasColumnType("NVARCHAR(100)").HasColumnName("Type");
                });
        }

        public override async Task<int> SaveChangesAsync(CancellationToken cancellationToken = new CancellationToken())
        {
            var aggregates = ChangeTracker.Entries<IAggregateRoot>().ToList();

            foreach (var aggregate in aggregates)
            {
                foreach (var domainEvent in aggregate.Entity.DomainEvents)
                {
                    await _publishEndpoint.Publish(domainEvent, cancellationToken);

                    //send event using RabbitMQ
                    //var json = JsonConvert.SerializeObject(domainEvent);
                    //Console.WriteLine(json);
                }
                aggregate.Entity.ClearEvents();
            }

            return await base.SaveChangesAsync(cancellationToken);
        }

        public DbSet<Student> Student { get; set; }
    }
}
