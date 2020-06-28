using System;
using System.Collections.Generic;
using System.Diagnostics.Tracing;
using System.Runtime.InteropServices;
using System.Text;
using EducationSystem.Common.ValueObjects;
using EducationSystem.StudentManagement.Core;
using FluentAssertions;
using MediatR;
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

        [Fact]
        public void Can_graduate_current_student()
        {
            var fullName = FullName.Create("Test", "Test").Value;
            var passport = Passport.Create("123123123").Value;
            var photo = PhotoUrl.Empty;
            var email = Email.Create("mail@mail.com").Value;
            var student = new Student(fullName, passport, photo, email);

            student.AssignToGroup(1);

            student.Graduate();
        }

        [Fact]
        public void Graduating_student_without_current_status_causes_an_exception()
        {
            var fullName = FullName.Create("Test", "Test").Value;
            var passport = Passport.Create("123123123").Value;
            var photo = PhotoUrl.Empty;
            var email = Email.Create("mail@mail.com").Value;
            var student = new Student(fullName, passport, photo, email);

            Action action = () => student.Graduate();

            action.Should().Throw<Exception>();
        }

        [Theory]
        [InlineData("0555555555", "mobile")]
        [InlineData("+994555555555", "home")]
        public void Can_add_phone_to_student(string number, string type)
        {
            var phone = Phone.Create(number, type).Value;

            var fullName = FullName.Create("Test", "Test").Value;
            var passport = Passport.Create("123123123").Value;
            var photo = PhotoUrl.Empty;
            var email = Email.Create("mail@mail.com").Value;
            var student = new Student(fullName, passport, photo, email);

            student.AddPhone(phone);
        }


        [Fact]
        public void Null_phone_add_to_student_throws_an_ArgumentNullException()
        {
            var fullName = FullName.Create("Test", "Test").Value;
            var passport = Passport.Create("123123123").Value;
            var photo = PhotoUrl.Empty;
            var email = Email.Create("mail@mail.com").Value;
            var student = new Student(fullName, passport, photo, email);

            Action action = () => student.AddPhone(null!);
            action.Should().ThrowExactly<ArgumentNullException>();
        }

       [Fact]
        public void Adding_existing_phone_to_student_causes_an_exception()
        {
            var phone = Phone.Create("0555555555", "mobile").Value;

            var fullName = FullName.Create("Test", "Test").Value;
            var passport = Passport.Create("123123123").Value;
            var photo = PhotoUrl.Empty;
            var email = Email.Create("mail@mail.com").Value;
            var student = new Student(fullName, passport, photo, email);

            student.AddPhone(phone); //add phone first time
            
            Action action = () => student.AddPhone(phone);

            action.Should().Throw<Exception>();
        }

        [Theory]
        [InlineData("0555555555", "mobile")]
        [InlineData("+994555555555", "home")]
        public void Can_remove_phone_from_student(string number, string type)
        {
            var phone = Phone.Create(number, type).Value;

            var fullName = FullName.Create("Test", "Test").Value;
            var passport = Passport.Create("123123123").Value;
            var photo = PhotoUrl.Empty;
            var email = Email.Create("mail@mail.com").Value;
            var student = new Student(fullName, passport, photo, email);

            student.AddPhone(phone);
            student.RemovePhone(phone.Number);
        }

        [Fact]
        public  void Removing_nonexistent_phone_causes_an_exception()
        {
            var fullName = FullName.Create("Test", "Test").Value;
            var passport = Passport.Create("123123123").Value;
            var photo = PhotoUrl.Empty;
            var email = Email.Create("mail@mail.com").Value;
            var student = new Student(fullName, passport, photo, email);

            Action action = () => student.RemovePhone("777888666");
            action.Should().Throw<Exception>();
        }

        [Fact]
        public void Null_phone_removing_throws_an_ArgumentNullException()
        {
            var fullName = FullName.Create("Test", "Test").Value;
            var passport = Passport.Create("123123123").Value;
            var photo = PhotoUrl.Empty;
            var email = Email.Create("mail@mail.com").Value;
            var student = new Student(fullName, passport, photo, email);

            Action action = () => student.RemovePhone(null!);
            action.Should().ThrowExactly<ArgumentNullException>();
        }

        [Theory]
        [InlineData("0555555555", "mobile", "0777777777", "personal")]
        [InlineData("+994555555555", "mobile", "0124444444", "home")]
        public void Can_change_phone_for_student(string oldNumber, string oldType, string newNumber, string newType)
        {
            var oldPhone = Phone.Create(oldNumber, oldType).Value;

            var fullName = FullName.Create("Test", "Test").Value;
            var passport = Passport.Create("123123123").Value;
            var photo = PhotoUrl.Empty;
            var email = Email.Create("mail@mail.com").Value;
            var student = new Student(fullName, passport, photo, email);

            student.AddPhone(oldPhone);
            var newPhone = Phone.Create(newNumber, newType).Value;
            student.ChangePhone(oldPhone, newPhone);
        }

        [Fact]
        public void Changing_nonexistent_phone_causes_an_exception()
        {
            var oldPhone = Phone.Create("1234567", "mobile").Value;

            var fullName = FullName.Create("Test", "Test").Value;
            var passport = Passport.Create("123123123").Value;
            var photo = PhotoUrl.Empty;
            var email = Email.Create("mail@mail.com").Value;
            var student = new Student(fullName, passport, photo, email);
            student.AddPhone(oldPhone);

            var newPhone = Phone.Create("555666777", "home").Value;

            var nonExistentPhone = Phone.Create("99988777", "personal").Value;

            Action action = () => student.ChangePhone(nonExistentPhone, newPhone);
            action.Should().Throw<Exception>();
        }


        [Fact]
        public void Null_phone_changing_throws_an_ArgumentNullException()
        {
            var fullName = FullName.Create("Test", "Test").Value;
            var passport = Passport.Create("123123123").Value;
            var photo = PhotoUrl.Empty;
            var email = Email.Create("mail@mail.com").Value;
            var student = new Student(fullName, passport, photo, email);

            Action action = () => student.ChangePhone(null!, null!);
            action.Should().ThrowExactly<ArgumentNullException>();
        }


        [Theory]
        [InlineData("Gleb", "Skripnikov", "Alexeevich")]
        [InlineData("Gleb", "Skripnikov", "")]
        public void Can_rename_existing_student(string firstName, string lastName, string middleName)
        {
            var fullName = FullName.Create("Test", "Test").Value;
            var passport = Passport.Create("123123123").Value;
            var photo = PhotoUrl.Empty;
            var email = Email.Create("mail@mail.com").Value;
            var student = new Student(fullName, passport, photo, email);

            var newFullName = FullName.Create(firstName, lastName, middleName).Value;

            student.Rename(newFullName);
        }

        [Fact]
        public void Null_fullName_changing_throws_an_ArgumentNullException()
        {
            var fullName = FullName.Create("Test", "Test").Value;
            var passport = Passport.Create("123123123").Value;
            var photo = PhotoUrl.Empty;
            var email = Email.Create("mail@mail.com").Value;
            var student = new Student(fullName, passport, photo, email);

            Action action = () => student.Rename(null!);
            action.Should().ThrowExactly<ArgumentNullException>();
        }

        [Fact]
        public void Can_change_passport_for_student()
        {
            var fullName = FullName.Create("Test", "Test").Value;
            var passport = Passport.Create("123123123").Value;
            var photo = PhotoUrl.Empty;
            var email = Email.Create("mail@mail.com").Value;
            var student = new Student(fullName, passport, photo, email);

            var newPassport = Passport.Create("uk777888").Value;
            student.ChangePassport(newPassport);
        }

        [Fact]
        public void Null_passport_changing_throws_an_ArgumentNullException()
        {
            var fullName = FullName.Create("Test", "Test").Value;
            var passport = Passport.Create("123123123").Value;
            var photo = PhotoUrl.Empty;
            var email = Email.Create("mail@mail.com").Value;
            var student = new Student(fullName, passport, photo, email);

            Action action = () => student.ChangePassport(null!);
            action.Should().ThrowExactly<ArgumentNullException>();
        }

        [Fact]
        public void Can_change_photoUrl_for_student()
        {
            var fullName = FullName.Create("Test", "Test").Value;
            var passport = Passport.Create("123123123").Value;
            var photo = PhotoUrl.Empty;
            var email = Email.Create("mail@mail.com").Value;
            var student = new Student(fullName, passport, photo, email);

            var newPhoto = PhotoUrl.Create("https://render.fineartamerica.com/images/rendered/default/metal-print/8/8/break/images/artworkimages/medium/1/tardis-art-painting-koko-priyanto.jpg").Value;
            student.ChangePhotoUrl(newPhoto);
        }

        [Fact]
        public void Null_photoUrl_changing_throws_an_ArgumentNullException()
        {
            var fullName = FullName.Create("Test", "Test").Value;
            var passport = Passport.Create("123123123").Value;
            var photo = PhotoUrl.Empty;
            var email = Email.Create("mail@mail.com").Value;
            var student = new Student(fullName, passport, photo, email);

            Action action = () => student.ChangePhotoUrl(null!);
            action.Should().ThrowExactly<ArgumentNullException>();
        }

        [Fact]
        public void Can_remove_photoUrl_from_student()
        {
            var fullName = FullName.Create("Test", "Test").Value;
            var passport = Passport.Create("123123123").Value;
            var photo = PhotoUrl.Create("https://render.fineartamerica.com/images/rendered/default/metal-print/8/8/break/images/artworkimages/medium/1/tardis-art-painting-koko-priyanto.jpg").Value;
            var email = Email.Create("mail@mail.com").Value;
            var student = new Student(fullName, passport, photo, email);

            student.RemovePhotoUrl();
        }

       [Fact]
        public void Can_change_email_for_student()
        {
            var fullName = FullName.Create("Test", "Test").Value;
            var passport = Passport.Create("123123123").Value;
            var photo = PhotoUrl.Empty;
            var email = Email.Create("mail@mail.com").Value;
            var student = new Student(fullName, passport, photo, email);

            var newEmail = Email.Create("test@mail.com").Value;
            student.ChangeEmail(newEmail);
        }

        [Fact]
        public void Null_email_changing_throws_an_ArgumentNullException()
        {
            var fullName = FullName.Create("Test", "Test").Value;
            var passport = Passport.Create("123123123").Value;
            var photo = PhotoUrl.Empty;
            var email = Email.Create("mail@mail.com").Value;
            var student = new Student(fullName, passport, photo, email);


            Action action = () => student.ChangeEmail(null!);
            action.Should().ThrowExactly<ArgumentNullException>();
        }

        [Fact]
        public void Can_remove_email_from_student()
        {
            var fullName = FullName.Create("Test", "Test").Value;
            var passport = Passport.Create("123123123").Value;
            var photo = PhotoUrl.Empty;
            var email = Email.Create("mail@mail.com").Value;
            var student = new Student(fullName, passport, photo, email);

            student.RemoveEmail();
        }

        [Fact]
        public void Can_assign_to_group_a_new_student()
        {
            var groupId = 1;
            var fullName = FullName.Create("Test", "Test").Value;
            var passport = Passport.Create("123123123").Value;
            var photo = PhotoUrl.Empty;
            var email = Email.Create("mail@mail.com").Value;
            var student = new Student(fullName, passport, photo, email);

            student.AssignToGroup(groupId);
        }
        [Fact]
        public void Cannot_assign_to_group_exposed_student()
        {
            var groupId = 1;
            var fullName = FullName.Create("Test", "Test").Value;
            var passport = Passport.Create("123123123").Value;
            var photo = PhotoUrl.Empty;
            var email = Email.Create("mail@mail.com").Value;
            var student = new Student(fullName, passport, photo, email);

            student.AssignToGroup(groupId); 
            student.Expose();
            Action action = () => student.AssignToGroup(groupId);
            action.Should().Throw<Exception>();
        }

        [Fact]
        public void Cannot_assign_to_group_graduated_student()
        {
            var groupId = 1;
            var fullName = FullName.Create("Test", "Test").Value;
            var passport = Passport.Create("123123123").Value;
            var photo = PhotoUrl.Empty;
            var email = Email.Create("mail@mail.com").Value;
            var student = new Student(fullName, passport, photo, email);

            student.AssignToGroup(groupId);
            student.Graduate();

            Action action = () => student.AssignToGroup(groupId);
            action.Should().Throw<Exception>();
        }

        [Theory]
        [InlineData(0)]
        [InlineData(-1)]
        public void Cannot_assign_to_group_student_if_groupId_less_or_equal_zero(int groupId)
        {
            var fullName = FullName.Create("Test", "Test").Value;
            var passport = Passport.Create("123123123").Value;
            var photo = PhotoUrl.Empty;
            var email = Email.Create("mail@mail.com").Value;
            var student = new Student(fullName, passport, photo, email);
           
            Action action = () => student.AssignToGroup(groupId);
            action.Should().Throw<Exception>();
        }

    }
}
