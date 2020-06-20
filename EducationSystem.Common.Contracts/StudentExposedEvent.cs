using EducationSystem.Common.Abstractions;

namespace EducationSystem.Common.Contracts
{
    public class StudentExposedEvent : IDomainEvent
    {
        public int StudentId { get; set; }
    }
}
