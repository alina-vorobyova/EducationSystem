using System.Collections.Generic;

namespace EducationSystem.Common.Abstractions
{
    public abstract class AggregateRoot<T> : Entity<T>, IAggregateRoot
    {
        private readonly List<IDomainEvent> _domainEvents = new List<IDomainEvent>();
        public IReadOnlyList<IDomainEvent> DomainEvents => _domainEvents;

        public void AddDomainEvent(IDomainEvent newEvent)
        {
            _domainEvents.Add(newEvent);
        }

        public void ClearEvents()
        {
            _domainEvents.Clear();
        }
    }
}
