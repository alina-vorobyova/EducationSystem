namespace EducationSystem.StudentManagement.Dtos
{
    public class NewStudentDto
    {
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string MiddleName { get; set; }
        public string Passport { get; set; }
        public string PhotoUrl { get; set; } = "";
        public string Email { get; set; } = "";
    }
}