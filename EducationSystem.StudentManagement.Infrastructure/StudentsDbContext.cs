using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using EducationSystem.Common.Abstractions;
using EducationSystem.StudentManagement.Core;
using MassTransit;
using Microsoft.EntityFrameworkCore;

namespace EducationSystem.StudentManagement.Infrastructure
{
    public class StudentsDbContext : DbContext
    {
        private readonly IBus _bus;

        public StudentsDbContext(DbContextOptions options, IBus bus) : base(options)
        {
            _bus = bus;
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
                .OwnsOne(x => x.Passport, x =>
                {
                    x.Property(y => y.Number).IsRequired().HasColumnType("NVARCHAR(100)").HasColumnName("Passport");
                })
                .OwnsOne(x => x.PhotoUrl, x =>
                {
                    x.Property(y => y.Url).IsRequired(false).HasColumnType("NVARCHAR(200)").HasColumnName("PhotoUrl");
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
                    await _bus.Publish(domainEvent, domainEvent.GetType(), cancellationToken);
                }
                aggregate.Entity.ClearEvents();
            }

            return await base.SaveChangesAsync(cancellationToken);
        }

        public DbSet<Student> Student { get; set; }
    }
}
