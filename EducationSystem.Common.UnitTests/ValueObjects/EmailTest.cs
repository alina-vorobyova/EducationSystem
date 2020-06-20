using System;
using System.Collections.Generic;
using System.Text;
using EducationSystem.Common.ValueObjects;
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
            new Email(emailString);
        }

        [Theory]
        [InlineData("qwerty123")]
        [InlineData("qwerty123@")]
        [InlineData("qwerty123@test")]
        [InlineData("qwerty123@test.")]
        [InlineData("@test.com")]
        [InlineData(null)]
        public void Invalid_email_creation_throws_an_argument_exception(string emailString)
        {
            Assert.Throws<ArgumentException>(() => new Email(emailString));
        }
    }
}
