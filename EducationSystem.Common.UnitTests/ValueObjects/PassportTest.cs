using System;
using System.Collections.Generic;
using System.Text;
using EducationSystem.Common.ValueObjects;
using FluentAssertions;
using Xunit;

namespace EducationSystem.Common.UnitTests.ValueObjects
{
    public class PassportTest
    {
        [Theory]
        [InlineData("aze666666")]
        [InlineData("uk777777")]
        public void Can_create_valid_passport(string number)
        {
            var fullName = Passport.Create(number);
            fullName.IsSuccess.Should().BeTrue();
        }


        [Theory]
        [InlineData("")]
        [InlineData(" ")]
        [InlineData("uk777777@")]
       public void Invalid_passport_creation_returns_failure_result(string number)
        {
            var passportResult = Passport.Create(number);
            passportResult.IsFailure.Should().BeTrue();
        }

       [Fact]
       public void Null_passport_creation_throws_an_ArgumentNullException()
       {
           Action action = () => Passport.Create(null!);
           action.Should().ThrowExactly<ArgumentNullException>();
       }
    }
}
