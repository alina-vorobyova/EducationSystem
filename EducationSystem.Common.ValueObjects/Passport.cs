using System;
using System.Collections.Generic;
using System.Net.WebSockets;
using System.Text;
using System.Text.RegularExpressions;
using EducationSystem.Common.Abstractions;
using EducationSystem.Common.Utils;

namespace EducationSystem.Common.ValueObjects
{
    public class Passport : ValueObject<Passport>
    {
        public string Number { get; } = string.Empty;

        protected Passport() { }

        protected Passport(string number) : this()
        {
           Number = number;
        }

        public static Result<Passport> Create(string number)
        {
            var regex = new Regex("^(?!^0+$)[a-zA-Z0-9]{3,20}$");

            if (!regex.IsMatch(number))
               return Result.Failure<Passport>("Passport number is invalid!");

            if (string.IsNullOrWhiteSpace(number))
                return Result.Failure<Passport>("Passport number can not be empty!");

            return Result.Success(new Passport(number));
        }

        protected override IEnumerable<object> GetEqualityComponents()
        {
            yield return Number;
        }
    }
}
