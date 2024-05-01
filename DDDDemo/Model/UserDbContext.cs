using MediatR;
using Microsoft.EntityFrameworkCore;

namespace DDDDemo.Model
{
    public class UserDbContext(DbContextOptions options, IMediator mediator) : BaseDbContext(options, mediator)
    {
        public DbSet<User> Users { get; set; }
    }
}
