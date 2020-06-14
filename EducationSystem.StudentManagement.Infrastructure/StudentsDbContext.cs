using System;
using System.Collections.Generic;
using System.Text;
using EducationSystem.StudentManagement.Core;
using Microsoft.EntityFrameworkCore;

namespace EducationSystem.StudentManagement.Infrastructure
{
    public class StudentsDbContext : DbContext
    {
        public StudentsDbContext(DbContextOptions options) : base(options)
        {
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

        public DbSet<Student> Student { get; set; }
    }
}
