using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using EducationSystem.Common.ValueObjects;
using EducationSystem.StudentManagement.Core;
using Microsoft.AspNetCore.Builder;
using Microsoft.Data.SqlClient;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using Polly;

namespace EducationSystem.StudentManagement.Infrastructure
{
    public static class DatabaseSeed
    {
        public static void InitializeDatabase(IApplicationBuilder app)
        {
            using var scope = app.ApplicationServices.GetService<IServiceScopeFactory>().CreateScope();
            var context = scope.ServiceProvider.GetRequiredService<StudentsDbContext>();

            var retryPolicy = Policy
                .Handle<SqlException>()
                .WaitAndRetry(5, retryAttempt => TimeSpan.FromSeconds(Math.Pow(2, retryAttempt)));

            retryPolicy.Execute(() => context.Database.Migrate());

            if (!context.Student.Any())
            {
                context.Student.Add(new Student(
                    new FullName("Gleb", "Skripnikov", "Alexeevich"),
                    new Passport("UA123123"),
                    PhotoUrl.Empty,
                    Email.Create("gspostmail@gmail.com").Value));

                context.Student.Add(new Student(
                    new FullName("Alina", "Skripnikova", "Andreyevna"),
                    new Passport("AZ999888"),
                    PhotoUrl.Empty,
                    Email.Create("musemuse67@gmail.com").Value));

                context.SaveChanges();
            }
        }
    }
}
