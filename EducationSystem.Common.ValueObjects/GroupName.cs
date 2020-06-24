using System;
using System.Collections.Generic;
using System.Text;
using System.Text.RegularExpressions;
using EducationSystem.Common.Abstractions;
using EducationSystem.Common.Utils;

namespace EducationSystem.Common.ValueObjects
{
    public class GroupName : ValueObject<GroupName>
    {
        public string Name { get; set; } = string.Empty;

        protected GroupName() { }

        protected GroupName(string name) : this()
        {
            Name = name;
        }

        public static Result<GroupName> Create(string name)
        {
            var regex = new Regex(@"^[a-zA-Z_0-9]{6,20}$");

            if (!regex.IsMatch(name))
               return Result.Failure<GroupName>("Group name is invalid!");

            if (string.IsNullOrWhiteSpace(name))
                return Result.Failure<GroupName>("Group name can not be empty!");

            return Result.Success(new GroupName(name));
        }

        protected override IEnumerable<object> GetEqualityComponents()
        {
            yield return Name;
        }
    }
}