using System;
using System.Collections.Generic;
using System.Text;
using EducationSystem.Common.ValueObjects;
using EducationSystem.StudentManagement.Core;
using Xunit;

namespace EducationSystem.StudentManagement.UnitTests.Core
{
    public class StudentTest
    {
        [Theory]
        [InlineData("Gleb", "Skripnikov", "", "UA123456", "https://google.com/dog.jpg", "gspostmail@gmail.com")]
        [InlineData("Alina", "Skripnikova", "Andreyevna", "AZE1234567", "https://google.com/cat.jpg", "musemuse67@gmail.com")]
        public void Can_create_a_valid_student(
            string firstName,
            string lastName,
            string middleName,
            string passportNumber,
            string photoUrl,
            string emailAddress)
        {
            var fullName = new FullName(firstName, lastName, middleName);
            var passport = new Passport(passportNumber);
            var photo = PhotoUrl.Create(photoUrl).Value;
            var email = Email.Create(emailAddress).Value;

            var student = new Student(fullName, passport, photo, email);

            Assert.NotNull(student);
        }


        [Fact]
        public void Cannot_create_an_invalid_student()
        {
            var fullName = new FullName("Test", "Test");
            var passport = new Passport("123123123");
            var photo = PhotoUrl.Empty;
            var email = Email.Create("mail@mail.com").Value;

            Assert.Throws<ArgumentNullException>(() =>
            {
                new Student(null, passport, photo, email);
                new Student(fullName, null, photo, email);
                new Student(fullName, passport, null, email);
                new Student(fullName, passport, photo, null);
            });
        }

        [Fact]
        public void Exposing_student_without_current_status_causes_an_exception()
        {
            var fullName = new FullName("Test", "Test");
            var passport = new Passport("123123123");
            var photo = PhotoUrl.Empty;
            var email = Email.Create("mail@mail.com").Value;
            var student = new Student(fullName, passport, photo, email);

            Assert.Throws<Exception>(() => student.Expose());
        }

        [Fact]
        public void Can_expose_current_student()
        {
            var fullName = new FullName("Test", "Test");
            var passport = new Passport("123123123");
            var photo = PhotoUrl.Empty;
            var email = Email.Create("mail@mail.com").Value;
            var student = new Student(fullName, passport, photo, email);

            student.AssignToGroup(1);

            student.Expose();
        }
    }
}
