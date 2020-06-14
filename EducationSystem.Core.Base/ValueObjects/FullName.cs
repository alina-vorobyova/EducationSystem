using System;
using System.Collections.Generic;
using System.Text;
using System.Text.RegularExpressions;

namespace EducationSystem.Core.Base.ValueObjects
{
    public class FullName : ValueObject<FullName>
    {
        public string FirstName { get; }
        public string LastName { get; }
        public string MiddleName { get; }

        public FullName(string firstName, string lastName, string middleName = "")
        {
            //var regex = new Regex("^[a-z ,.'-]+$", RegexOptions.IgnoreCase);
            var regex = new Regex(@"^[\w'\-,.][^0-9_!¡?÷?¿/\\+=@#$%ˆ&*(){}|~<>;:[\]]{2,}$", RegexOptions.IgnoreCase);

            if (!regex.IsMatch(firstName))
                throw new ArgumentException("First name is invalid");

            if (!regex.IsMatch(lastName))
                throw new ArgumentException("Last name is invalid");

            if (middleName != "" && !regex.IsMatch(middleName))
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
