using System;
using System.Collections.Generic;
using System.Text;
using System.Text.RegularExpressions;
using EducationSystem.Common.Abstractions;

namespace EducationSystem.Common.ValueObjects
{
    public class Phone : ValueObject<Phone>
    {
        public string Number { get; } = string.Empty;
        public string Type { get; } = string.Empty;

        protected Phone() { }

        public Phone(string number, string type)
        {
            var regex = new Regex(@"^([+]?[\s0-9]+)?(\d{3}|[(]?[0-9]+[)])?([-]?[\s]?[0-9])+$");

            if (!regex.IsMatch(number))
                throw new ArgumentException("Phone number is invalid");

            if (string.IsNullOrWhiteSpace(type))
                throw new ArgumentException("Phone type can't be empty");

            Number = number;
            Type = type;
        }

        protected override IEnumerable<object> GetEqualityComponents()
        {
            yield return Number;
            yield return Type;
        }
    }
}
