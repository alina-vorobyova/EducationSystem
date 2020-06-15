using System;
using System.Collections.Generic;
using System.Net.WebSockets;
using System.Text;
using System.Text.RegularExpressions;
using EducationSystem.Common.Abstractions;

namespace EducationSystem.Common.ValueObjects
{
    public class Passport : ValueObject<Passport>
    {
        public string Number { get; }

        protected Passport() { }

        public Passport(string number)
        {
            var regex = new Regex("^(?!^0+$)[a-zA-Z0-9]{3,20}$");

            if(!regex.IsMatch(number))
                throw new ArgumentException("Passport number is invalid!");

            if(string.IsNullOrWhiteSpace(number))
                throw new ArgumentException("Passport number can not be empty!");

            Number = number;
        }

        protected override IEnumerable<object> GetEqualityComponents()
        {
            yield return Number;
        }
    }
}
