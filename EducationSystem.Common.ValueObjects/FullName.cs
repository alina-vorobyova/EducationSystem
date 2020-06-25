using System;
using System.Collections.Generic;
using System.Text.RegularExpressions;
using EducationSystem.Common.Abstractions;
using EducationSystem.Common.Utils;

namespace EducationSystem.Common.ValueObjects
{
    public class FullName : ValueObject<FullName>
    {
        public string FirstName { get; } = string.Empty;
        public string LastName { get; } = string.Empty;
        public string MiddleName { get; } = string.Empty;

        protected FullName() { }

        protected FullName(string firstName, string lastName, string middleName = "") : this()
        {
            FirstName = firstName;
            LastName = lastName;
            MiddleName = middleName;
        }


        public static Result<FullName> Create(string firstName, string lastName, string middleName = "")
        {
            var regex = new Regex(@"^[\w'\-,.][^0-9_!¡?÷?¿/\\+=@#$%ˆ&*(){}|~<>;:[\]]{2,}$", RegexOptions.IgnoreCase);

            if (!regex.IsMatch(firstName))
                return Result.Failure<FullName>("First name is invalid");

            if (string.IsNullOrWhiteSpace(firstName))
                return Result.Failure<FullName>("First name can not be empty");

            if (!regex.IsMatch(lastName))
                return Result.Failure<FullName>("Last name is invalid");

            if (string.IsNullOrWhiteSpace(lastName))
                return Result.Failure<FullName>("Last name can not be empty");

            if (middleName != string.Empty && !regex.IsMatch(middleName))
                return Result.Failure<FullName>("Middle name is invalid");

            return Result.Success(new FullName(firstName, lastName, middleName));
        }


        protected override IEnumerable<object> GetEqualityComponents()
        {
            yield return FirstName;
            yield return LastName;
            yield return MiddleName;
        }
    }
}
