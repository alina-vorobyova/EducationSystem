using System;
using System.Collections.Generic;
using System.Text;
using EducationSystem.Common.ValueObjects;
using FluentAssertions;
using Xunit;

namespace EducationSystem.Common.UnitTests.ValueObjects
{
    public class PhoneTest
    {
        [Theory]
        [InlineData("0127777777", "home")]
        [InlineData("0555555555", "work")]
        [InlineData("+994777777777", "mobile")]
        public void Can_create_valid_phone(string number, string type)
        {
            var phoneResult = Phone.Create(number, type);
            phoneResult.IsSuccess.Should().BeTrue();
        }

        [Theory]
        [InlineData("", "")]
        [InlineData(" ", " ")]
        [InlineData(" ", "test")]
        [InlineData("7777777777", " ")]
        [InlineData("7777777777", "")]
        [InlineData("", "home")]
        [InlineData("7777777@" , "test")]
        [InlineData("7777777a" , "test")]
        public void Invalid_phone_creation_returns_failure_result(string number, string type)
        {
            var phoneResult = Phone.Create(number, type);
            phoneResult.IsFailure.Should().BeTrue();
        }

        [Fact]
        public void Null_phone_creation_throws_an_ArgumentNullException()
        {
            Action action = () => Phone.Create(null!, null!);
            action.Should().ThrowExactly<ArgumentNullException>();
        }
    }
}
