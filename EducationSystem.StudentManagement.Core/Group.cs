using System;
using System.Collections.Generic;
using System.Text;
using EducationSystem.Common.Abstractions;
using EducationSystem.Common.ValueObjects;

namespace EducationSystem.StudentManagement.Core
{
    public class Group : Entity<int>
    {
        public GroupName GroupName { get; set; }

        protected Group() { }

        public Group(GroupName groupName)
        {
            if(groupName is null)
                throw new Exception("Group name can not be null!");

            GroupName = groupName;
        }

    }
}
