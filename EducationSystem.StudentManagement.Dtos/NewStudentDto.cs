namespace EducationSystem.StudentManagement.Dtos
{
    public class NewStudentDto
    {
        public string FirstName { get; set; } = string.Empty;
        public string LastName { get; set; } = string.Empty;
        public string? MiddleName { get; set; }
        public string Passport { get; set; } = string.Empty;
        public string? PhotoUrl { get; set; }
        public string? Email { get; set; }
    }
}