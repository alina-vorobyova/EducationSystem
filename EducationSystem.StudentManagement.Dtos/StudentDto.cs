using System;
using System.Collections.Generic;

namespace EducationSystem.StudentManagement.Dtos
{
    public class StudentDto
    {
        public int Id { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string MiddleName { get; set; }
        public string Status { get; set; }
        public string Passport { get; set; }
        public string PhotoUrl { get; set; }
        public string Email { get; set; }
        public int GroupId { get; set; }
        public ICollection<PhoneDto> Phones { get; set; }
    }

    public class PhoneDto
    {
        public string Number { get; set; }
        public string Type { get; set; }
    }
}
