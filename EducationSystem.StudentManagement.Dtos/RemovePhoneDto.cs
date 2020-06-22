using System;
using System.Collections.Generic;
using System.Text;

namespace EducationSystem.StudentManagement.Dtos
{
    public class RemovePhoneDto
    {
        public int StudentId { get; set; }
        public string Number { get; set; }
        public string Type { get; set; }
    }
}
