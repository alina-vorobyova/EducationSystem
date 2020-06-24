using System;
using System.Collections.Generic;
using System.Text;
using System.Text.RegularExpressions;
using EducationSystem.Common.Abstractions;
using EducationSystem.Common.Utils;

namespace EducationSystem.Common.ValueObjects
{
    public class Phone : ValueObject<Phone>
    {
        public string Number { get; } = string.Empty;
        public string Type { get; } = string.Empty;

        protected Phone() { }

        protected Phone(string number, string type) : this()
        {
            Number = number;
            Type = type;
        }

        public static Result<Phone> Create(string number, string type)
        {
            var regex = new Regex(@"^([+]?[\s0-9]+)?(\d{3}|[(]?[0-9]+[)])?([-]?[\s]?[0-9])+$");

            if (!regex.IsMatch(number))
                return Result.Failure<Phone>("Phone number is invalid");

            if (string.IsNullOrWhiteSpace(type))
                return Result.Failure<Phone>("Phone type can't be empty");

            return Result.Success(new Phone(number, type));
        }

        protected override IEnumerable<object> GetEqualityComponents()
        {
            yield return Number;
            yield return Type;
        }
    }
}
