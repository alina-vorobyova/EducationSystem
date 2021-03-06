﻿using System;
using System.Collections.Generic;
using System.Text;
using System.Text.RegularExpressions;
using EducationSystem.Common.Abstractions;
using EducationSystem.Common.Utils;

namespace EducationSystem.Common.ValueObjects
{
    public class Email : ValueObject<Email>
    {
        public static Email Empty => new Email();
        
        public string EmailAddress { get; } = string.Empty;

        protected Email() {}

        protected Email(string emailAddress) : this()
        {
            EmailAddress = emailAddress;
        }

        public static Result<Email> Create(string emailAddress)
        {
            var regex = new Regex(@"^\w+([-+.']\w+)*@\w+([-.]\w+)*\.\w+([-.]\w+)*$");

            if (string.IsNullOrWhiteSpace(emailAddress))
                return Result.Success(Email.Empty);

            if (!regex.IsMatch(emailAddress))
                return Result.Failure<Email>("Email address is invalid");

            return Result.Success(new Email(emailAddress));
        }

        protected override IEnumerable<object> GetEqualityComponents()
        {
            yield return EmailAddress;
        }
    }
}
