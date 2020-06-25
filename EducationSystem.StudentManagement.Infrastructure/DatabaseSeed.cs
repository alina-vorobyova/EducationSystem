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
                    FullName.Create("Gleb", "Skripnikov", "Alexeevich").Value,
                    Passport.Create("UA123123").Value,
                    PhotoUrl.Empty,
                    Email.Create("gspostmail@gmail.com").Value));

                context.Student.Add(new Student(
                    FullName.Create("Alina", "Skripnikova", "Andreyevna").Value,
                    Passport.Create("AZ999888").Value,
                    PhotoUrl.Empty,
                    Email.Create("musemuse67@gmail.com").Value));

                context.SaveChanges();
            }
        }
    }
}
