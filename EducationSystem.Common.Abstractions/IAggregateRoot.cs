using System.Collections.Generic;

namespace EducationSystem.Common.Abstractions
{
    public interface IAggregateRoot
    {
        IReadOnlyList<IDomainEvent> DomainEvents { get; }
        void AddDomainEvent(IDomainEvent newEvent);
        public void ClearEvents();
    }
}