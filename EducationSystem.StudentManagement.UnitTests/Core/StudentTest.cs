using System;
using System.Collections.Generic;
using System.Text;
using EducationSystem.Common.ValueObjects;
using EducationSystem.StudentManagement.Core;
using FluentAssertions;
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
            var fullName = FullName.Create(firstName, lastName, middleName).Value;
            var passport = Passport.Create(passportNumber).Value;
            var photo = PhotoUrl.Create(photoUrl).Value;
            var email = Email.Create(emailAddress).Value;

            new Student(fullName, passport, photo, email).Should().NotBeNull();
        }


        [Fact]
        public void Cannot_create_an_invalid_student()
        {
            var fullName = FullName.Create("Test", "Test").Value;
            var passport = Passport.Create("123123123").Value;
            var photo = PhotoUrl.Empty;
            var email = Email.Create("mail@mail.com").Value;

            Action action = () =>
            {
                new Student(null!, passport, photo, email);
                new Student(fullName, null!, photo, email);
                new Student(fullName, passport, null!, email);
                new Student(fullName, passport, photo, null!);
            };

            action.Should().ThrowExactly<ArgumentNullException>();
        }

        [Fact]
        public void Exposing_student_without_current_status_causes_an_exception()
        {
            var fullName = FullName.Create("Test", "Test").Value;
            var passport = Passport.Create("123123123").Value;
            var photo = PhotoUrl.Empty;
            var email = Email.Create("mail@mail.com").Value;
            var student = new Student(fullName, passport, photo, email);

            Action action = () => student.Expose();

            action.Should().Throw<Exception>();
        }

        [Fact]
        public void Can_expose_current_student()
        {
            var fullName = FullName.Create("Test", "Test").Value;
            var passport = Passport.Create("123123123").Value;
            var photo = PhotoUrl.Empty;
            var email = Email.Create("mail@mail.com").Value;
            var student = new Student(fullName, passport, photo, email);

            student.AssignToGroup(1);

            student.Expose();
        }
    }
}
