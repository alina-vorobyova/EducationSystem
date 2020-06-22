using System.Linq;
using System.Reflection;
using System.Threading;
using System.Threading.Tasks;
using EducationSystem.Common.Abstractions;
using EducationSystem.StudentManagement.Core;
using MassTransit;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;

namespace EducationSystem.StudentManagement.Infrastructure
{
    public class StudentsDbContext : DbContext
    {
        private readonly IBus _bus;

        public StudentsDbContext(DbContextOptions options) : base(options)
        {
            _bus = this.GetService<IBus>();
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);
            modelBuilder.ApplyConfigurationsFromAssembly(Assembly.GetExecutingAssembly());
        }

        public override async Task<int> SaveChangesAsync(CancellationToken cancellationToken = new CancellationToken())
        {
            var aggregates = ChangeTracker.Entries<IAggregateRoot>().ToList();

            foreach (var aggregate in aggregates)
            {
                foreach (var domainEvent in aggregate.Entity.DomainEvents)
                {
                    await _bus.Publish(domainEvent, domainEvent.GetType(), cancellationToken);
                }
                aggregate.Entity.ClearEvents();
            }

            return await base.SaveChangesAsync(cancellationToken);
        }

        public DbSet<Student> Student { get; set; } = null!;
    }
}
