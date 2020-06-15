﻿using System;
using System.Collections.Generic;
using System.Text;
using EducationSystem.Common.Abstractions;

namespace EducationSystem.StudentManagement.Core.DomainEvents
{
    public class StudentExposedEvent : IDomainEvent
    {
        public int StudentId { get; set; }
    }
}
