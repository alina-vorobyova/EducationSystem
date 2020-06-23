namespace EducationSystem.StudentManagement.Dtos
{
    public class NewStudentDto
    {
        public string FirstName { get; set; } = string.Empty;
        public string LastName { get; set; } = string.Empty;
        public string MiddleName { get; set; } = string.Empty;
        public string Passport { get; set; } = string.Empty;
        public string? PhotoUrl { get; set; } = string.Empty;
        public string? Email { get; set; } = string.Empty;
    }
}