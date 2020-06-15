using System;
using System.Collections.Generic;
using System.Text;
using EducationSystem.Common.Abstractions;

namespace EducationSystem.StudentManagement.Core.DomainEvents
{
    class StudentExposedEvent : IDomainEvent
    {
        public int StudentId { get; }

        public StudentExposedEvent(int studentId)
        {
            StudentId = studentId;
        }
    }
}
