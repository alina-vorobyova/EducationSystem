using System;
using System.Collections.Generic;
using System.Text;
using System.Text.RegularExpressions;
using EducationSystem.Common.Abstractions;

namespace EducationSystem.Common.ValueObjects
{
    public class GroupName : ValueObject<GroupName>
    {
        public string Name { get; set; }

        protected GroupName() { }

        public GroupName(string name)
        {
            var regex = new Regex(@"^[a-zA-Z_0-9]{6,20}$");

            if (!regex.IsMatch(name))
                throw new ArgumentException("Group name is invalid!");

            if (string.IsNullOrWhiteSpace(name))
                throw new ArgumentException("Group name can not be empty!");

            Name = name;
        }

        protected override IEnumerable<object> GetEqualityComponents()
        {
            yield return Name;
        }
    }
}