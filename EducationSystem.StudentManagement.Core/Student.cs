using System;
using System.Collections.Generic;
using EducationSystem.Common.Abstractions;
using EducationSystem.Common.Contracts;
using EducationSystem.Common.ValueObjects;

namespace EducationSystem.StudentManagement.Core
{
    public class Student : AggregateRoot<int>
    {
        public FullName FullName { get; private set; }
        public StudentStatus Status { get; private set; }
        public List<Phone> Phones { get; private set; }

        protected Student() { }

        public Student(FullName fullName)
        {
            if (fullName is null)
                throw new Exception("Can't create student without a name!");

            Id = default;
            FullName = fullName;
            Status = StudentStatus.New;
            Phones = new List<Phone>();
        }

        public void Expose()
        {
            if (Status != StudentStatus.Current)
                throw new Exception("Can't expose not current student!");

            Status = StudentStatus.Exposed;

            AddDomainEvent(new StudentExposedEvent { StudentId = Id });
        }

        public void AddPhone(Phone phone)
        {
            Phones.Add(phone);
        }
    }
}
