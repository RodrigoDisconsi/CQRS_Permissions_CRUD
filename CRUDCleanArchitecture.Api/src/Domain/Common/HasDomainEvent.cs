using System.ComponentModel.DataAnnotations.Schema;
using Nest;

namespace CRUDCleanArchitecture.Domain.Common;
public abstract class HasDomainEvent
{
    private readonly List<BaseEvent> _domainEvents = new();

    [NotMapped]
    [Ignore]
    public IReadOnlyCollection<BaseEvent> DomainEvents => _domainEvents.AsReadOnly();

    public void AddDomainEvent(BaseEvent domainEvent)
    {
        _domainEvents.Add(domainEvent);
    }

    public void RemoveDomainEvent(BaseEvent domainEvent)
    {
        _domainEvents.Remove(domainEvent);
    }

    public void ClearDomainEvents()
    {
        _domainEvents.Clear();
    }
}
