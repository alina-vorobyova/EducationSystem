using System;
using System.Collections.Generic;
using System.Text;
using EducationSystem.Common.ValueObjects;
using FluentAssertions;
using Xunit;

namespace EducationSystem.Common.UnitTests.ValueObjects
{
    public class PhotoUrlTest
    {
        [Theory]
        [InlineData("")]
        [InlineData(" ")]
        [InlineData("https://i.pinimg.com/originals/68/a8/d7/68a8d7ce34e4914aef4904367d97b894.jpg")]
        [InlineData("http://cached.imagescaler.hbpl.co.uk/resize/scaleHeight/815/cached.offlinehbpl.hbpl.co.uk/news/OMC/HTCDOCTORWHO-2019092301502914.jpg")]
        [InlineData("https://fsa.zobj.net/crop.php?r=HQx7mDwBpvZ3hISTDWyb2Cc08zDOSQodeZ0_Sq-gsj9rgo7wMb840DnLbaigslFS2nXllQndKKHUXT1BTn6VtxcSAdkPonRd3VK4bzl34rq4r7BUCdYWKG2FxpnrcOE09yqFUXwfHuaxMZKF")]
        public void Can_photoUrl_valid_passport(string url)
        {
            var photoResult = PhotoUrl.Create(url);
            photoResult.IsSuccess.Should().BeTrue();
        }

        [Theory]
        [InlineData("#https://i.pinimg.com/originals/68/a8/d7/68a8d7ce34e4914aef4904367d97b894.jpg")]
        [InlineData("@https://i.pinimg.com/originals/68/a8/d7/68a8d7ce34e4914aef4904367d97b894.jpg")]
        [InlineData(".https://i.pinimg.com/originals/68/a8/d7/68a8d7ce34e4914aef4904367d97b894.jpg")]
        [InlineData("blablablahttp://i.pinimg.com/originals/68/a8/d7/68a8d7ce34e4914aef4904367d97b894.jpg")]
        public void Invalid_photoUrl_creation_returns_failure_result(string url)
        {
            var photoResult = PhotoUrl.Create(url);
            photoResult.IsFailure.Should().BeTrue();
        }


        [Fact]
        public void Null_photoUrl_creation_throws_an_ArgumentNullException()
        {
            Action action = () => PhotoUrl.Create(null!);
            action.Should().ThrowExactly<ArgumentNullException>();
        }
    }
}
