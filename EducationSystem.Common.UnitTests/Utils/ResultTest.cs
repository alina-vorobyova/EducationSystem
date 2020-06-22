using System;
using System.Collections.Generic;
using System.Text;
using System.Threading;
using EducationSystem.Common.Utils;
using FluentAssertions;
using Xunit;

namespace EducationSystem.Common.UnitTests.Utils
{
    public class ResultTest
    {
        [Fact]
        public void Combining_two_success_results_returns_a_success_result()
        {
            var resultOne = Result.Success();
            var resultTwo = Result.Success();

            var result = Result.Combine(resultOne, resultTwo);

            result.IsSuccess.Should().BeTrue();
            result.IsFailure.Should().BeFalse();
        }

        [Fact]
        public void Combining_two_failure_results_returns_a_failure_result()
        {
            var resultOne = Result.Failure(string.Empty);
            var resultTwo = Result.Failure(string.Empty);

            var result = Result.Combine(resultOne, resultTwo);

            result.IsSuccess.Should().BeFalse();
            result.IsFailure.Should().BeTrue();
        }

        [Fact]
        public void Combining_success_and_failure_results_returns_a_failure_result()
        {
            var resultOne = Result.Failure(string.Empty);
            var resultTwo = Result.Success();

            var result = Result.Combine(resultOne, resultTwo);

            result.IsSuccess.Should().BeFalse();
            result.IsFailure.Should().BeTrue();
        }

        [Fact]
        public void Combining_two_failure_results_returns_a_result_with_all_errors()
        {
            var resultOne = Result.Failure("One");
            var resultTwo = Result.Failure("Two");

            var result = Result.Combine(resultOne, resultTwo);

            result.ErrorMessage.Should().Be("One Two");
        }
    }
}
