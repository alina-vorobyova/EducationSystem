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
        [InlineData("Alina", "Skripnikova", "Andreyevna")]
        [InlineData("Gleb", "Skripnikov", "Alexeyevich")]
        public void Can_create_valid_fullname(string firstName, string lastName, string middleName)
        {
            var fullName = FullName.Create(firstName, lastName, middleName);
            fullName.IsSuccess.Should().BeTrue();
        }
    }
}
