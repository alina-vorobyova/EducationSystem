using System;
using System.Collections.Generic;
using System.Text;
using EducationSystem.StudentManagement.Core;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace EducationSystem.StudentManagement.Infrastructure.Configurations
{
    class StudentConfiguration : IEntityTypeConfiguration<Student>
    {
        public void Configure(EntityTypeBuilder<Student> builder)
        {
            builder
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
    }
}
