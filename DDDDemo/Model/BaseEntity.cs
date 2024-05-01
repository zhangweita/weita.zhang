using DDDDemo.Event;
using MediatR;

namespace DDDDemo.Model;

public class BaseEntity : IDomainEvents
{
    private List<INotification> DomainEvents = new();

    public void AddDomainEvent(INotification notification) => DomainEvents.Add(notification);

    public void AddDomainEventIfAbsent(INotification notification)
    {
        if (!DomainEvents.Contains(notification))
        {
            DomainEvents.Add(notification);
        }
    }
    public void ClearDomainEvents() => DomainEvents.Clear();

    public IEnumerable<INotification> GetDomainEvents() => DomainEvents;
}
