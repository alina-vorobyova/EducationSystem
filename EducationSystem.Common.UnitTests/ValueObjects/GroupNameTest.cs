using System;
using System.Collections.Generic;
using System.Text;
using EducationSystem.Common.ValueObjects;
using FluentAssertions;
using Xunit;

namespace EducationSystem.Common.UnitTests.ValueObjects
{
    public class GroupNameTest
    {
        [Theory]
        [InlineData("Test_1234")]
        [InlineData("Test_123456")]
        [InlineData("test_123456")]
        [InlineData("Test1234")]
        public void Can_create_valid_fullname(string name)
        {
            var groupName = GroupName.Create(name);
            groupName.IsSuccess.Should().BeTrue();
        }

        [Theory]
        [InlineData("")]
        [InlineData(" ")]
        [InlineData("Test")]
        [InlineData("1234")]
        [InlineData("Test_1234@")]
        public void Invalid_groupName_creation_returns_failure_result(string name)
        {
            var groupResult = GroupName.Create(name);
            groupResult.IsFailure.Should().BeTrue();
        }

        [Fact]
        public void Null_groupName_creation_throws_an_ArgumentNullException()
        {
            Action action = () => GroupName.Create(null!);
            action.Should().ThrowExactly<ArgumentNullException>();
        }
    }
}
