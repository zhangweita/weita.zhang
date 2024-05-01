using MediatR;

namespace DDDDemo.Event;

public interface IDomainEvents
{
    IEnumerable<INotification> GetDomainEvents();   //获取注册的领域事件
    void AddDomainEvent(INotification notification);    //注册领域事件
    void AddDomainEventIfAbsent(INotification notification);    //如果领域事件不存在，则注册
    void ClearDomainEvents();   // 清除注册的领域事件
}
