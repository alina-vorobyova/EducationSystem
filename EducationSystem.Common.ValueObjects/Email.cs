using System;
using System.Collections.Generic;
using System.Text;
using System.Text.RegularExpressions;
using EducationSystem.Common.Abstractions;

namespace EducationSystem.Common.ValueObjects
{
    public class Email : ValueObject<Email>
    {
        public string EmailAddress { get; private set; }
        public static Email Empty => new Email { EmailAddress = string.Empty };

        protected Email() {}

        public Email(string emailAddress)
        {
            var regex = new Regex(@"^\w+([-+.']\w+)*@\w+([-.]\w+)*\.\w+([-.]\w+)*$");

            if (emailAddress is null)
                throw new ArgumentException("Email address cannot be null");

            if (!regex.IsMatch(emailAddress))
                throw new ArgumentException("Email address is invalid");

            EmailAddress = emailAddress;
        }

        protected override IEnumerable<object> GetEqualityComponents()
        {
            yield return EmailAddress;
        }
    }
}
