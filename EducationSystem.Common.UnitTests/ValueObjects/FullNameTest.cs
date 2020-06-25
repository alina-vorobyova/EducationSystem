using System;
using System.Collections.Generic;
using System.Net.WebSockets;
using System.Text;
using EducationSystem.Common.ValueObjects;
using FluentAssertions;
using Xunit;

namespace EducationSystem.Common.UnitTests.ValueObjects
{
    public class FullNameTest
    {

        [Theory]
        [InlineData("Test", "Test", "Test")]
        [InlineData("Test", "Test", "")]
        public void Can_create_valid_fullname(string firstName, string lastName, string middleName)
        {
            var fullName = FullName.Create(firstName, lastName, middleName);
            fullName.IsSuccess.Should().BeTrue();
        }

        [Theory]
        [InlineData("1234", "1234", "1234")]
        [InlineData("1234", "Test", "Test")]
        [InlineData("Test", "1234", "Test")]
        [InlineData("Test", "Test", "1234")]
        [InlineData("Test1", "Test", "Test")]    
        [InlineData("Test", "Test1", "Test")]
        [InlineData("Test", "Test", "Test1")]
        [InlineData("Test@", "Test", "Test1")]
        [InlineData("Test", "Test@", "Test1")]
        [InlineData("Test", "Test", "Test1@")]
        [InlineData(" ", "Test", "Test")]
        public void Invalid_fullname_creation_returns_failure_result(string firstName, string lastName, string middleName)
        {
            var fullNameResult = FullName.Create(firstName, lastName, middleName);
            fullNameResult.IsFailure.Should().BeTrue();
        }
         
        [Fact]
        public void Null_fullname_creation_throws_an_ArgumentNullException()
        {
            Action action = () => FullName.Create(null!, null!, null!);
            action.Should().ThrowExactly<ArgumentNullException>();
        }
    }
}
