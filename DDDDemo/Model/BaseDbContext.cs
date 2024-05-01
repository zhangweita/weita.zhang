using DDDDemo.Event;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace DDDDemo.Model;

public class BaseDbContext(DbContextOptions options, IMediator mediator) : DbContext(options)
{
    private readonly IMediator mediator = mediator;

    public override int SaveChanges()
    {
        // 项目中要求不用同步保存，所以设置抛出异常
        throw new NotImplementedException("Don't call SaveChanges");
    }

    public override async Task<int> SaveChangesAsync(bool acceptAllChangesOnSuccess, CancellationToken cancellationToken = default)
    {
        var domainEntities = this.ChangeTracker.Entries<IDomainEvents>().Where(x => x.Entity.GetDomainEvents().Any());

        var domainEvents = domainEntities.SelectMany(x => x.Entity.GetDomainEvents()).ToList();

        foreach (var domainEvent in domainEvents)
        {
            await mediator.Publish(domainEvent);
        }

        return await base.SaveChangesAsync(acceptAllChangesOnSuccess, cancellationToken);
    }
}
