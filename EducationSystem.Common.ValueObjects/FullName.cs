﻿using System;
using System.Collections.Generic;
using System.Text.RegularExpressions;
using EducationSystem.Common.Abstractions;

namespace EducationSystem.Common.ValueObjects
{
    public class FullName : ValueObject<FullName>
    {
        public string FirstName { get; }
        public string LastName { get; }
        public string MiddleName { get; }

        protected FullName() { }

        public FullName(string firstName, string lastName, string middleName = "")
        {
            //var regex = new Regex("^[a-z ,.'-]+$", RegexOptions.IgnoreCase);
            var regex = new Regex(@"^[\w'\-,.][^0-9_!¡?÷?¿/\\+=@#$%ˆ&*(){}|~<>;:[\]]{2,}$", RegexOptions.IgnoreCase);

            if (!regex.IsMatch(firstName))
                throw new ArgumentException("First name is invalid");

            if(string.IsNullOrWhiteSpace(firstName))
                throw new ArgumentException("First name can not be empty");

            if (!regex.IsMatch(lastName))
                throw new ArgumentException("Last name is invalid");

            if (string.IsNullOrWhiteSpace(lastName))
                throw new ArgumentException("Last name can not be empty");

            if (middleName != string.Empty && !regex.IsMatch(middleName))
                throw new ArgumentException("Middle name is invalid");

            FirstName = firstName;
            LastName = lastName;
            MiddleName = middleName;
        }

        protected override IEnumerable<object> GetEqualityComponents()
        {
            yield return FirstName;
            yield return LastName;
            yield return MiddleName;
        }
    }
}
