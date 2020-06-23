using System;
using System.Collections.Generic;
using System.Text;

namespace EducationSystem.StudentManagement.Dtos
{
    public class UpdateStudentDto
    {
        public int Id { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string MiddleName { get; set; }
        public string Passport { get; set; }
        public string PhotoUrl { get; set; }
        public string Email { get; set; }
    }
}
