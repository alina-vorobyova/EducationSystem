using System;
using System.Collections.Generic;
using System.Text;
using EducationSystem.Common.ValueObjects;
using FluentAssertions;
using Xunit;

namespace EducationSystem.Common.UnitTests.ValueObjects
{
    public class EmailTest
    {
        [Theory]
        [InlineData("gspostmail@gmail.com")]
        [InlineData("musemuse67@gmail.com")]
        public void Can_create_valid_email(string emailString)
        {
            var emailResult = Email.Create(emailString);
            emailResult.IsSuccess.Should().BeTrue();
        }

        [Theory]
        [InlineData("qwerty123")]
        [InlineData("qwerty123@")]
        [InlineData("qwerty123@test")]
        [InlineData("qwerty123@test.")]
        [InlineData("@test.com")]
        public void Invalid_email_creation_returns_failure_result(string emailString)
        {
            var emailResult = Email.Create(emailString);
            emailResult.IsFailure.Should().BeTrue();
        }

        [Fact]
        public void Null_email_creation_throws_an_ArgumentNullException()
        {
            Action action = () => Email.Create(null!);
            action.Should().ThrowExactly<ArgumentNullException>();
        }
    }
}
