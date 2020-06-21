using System;
using System.Collections.Generic;
using System.Text;
using System.Text.RegularExpressions;
using EducationSystem.Common.Abstractions;
using EducationSystem.Common.Utils;

namespace EducationSystem.Common.ValueObjects
{
    public class Email : ValueObject<Email>
    {
        public string EmailAddress { get; private set; }
        public static Email Empty => new Email(string.Empty);

        protected Email() {}

        protected Email(string emailAddress) : this()
        {
            EmailAddress = emailAddress;
        }

        public static Result<Email> Create(string emailAddress)
        {
            var regex = new Regex(@"^\w+([-+.']\w+)*@\w+([-.]\w+)*\.\w+([-.]\w+)*$");

            if (emailAddress is null)
                return Result<Email>.Failure("Email address cannot be null");

            if (!regex.IsMatch(emailAddress))
                return Result<Email>.Failure("Email address is invalid");

            return Result<Email>.Success(new Email(emailAddress));
        }

        protected override IEnumerable<object> GetEqualityComponents()
        {
            yield return EmailAddress;
        }
    }
}
