using BlogFlow.Auth.Application.Interface.Persistence;
using BlogFlow.Auth.Persistence.Contexts;

namespace BlogFlow.Auth.Persistence.Repositories
{
    public class UnitOfWork : IUnitOfWork
    {
        public IUsersRepository Users { get; }
        private readonly ApplicationDbContext _applicaDbContext;

        public UnitOfWork(IUsersRepository users, ApplicationDbContext applicationDbContext)
        {
            Users = users;
            _applicaDbContext = applicationDbContext;
        }

        public async Task<int> Save(CancellationToken cancellationToken)
        {
            return await _applicaDbContext.SaveChangesAsync(cancellationToken);
        }

        public void Dispose()
        {
            GC.SuppressFinalize(this);
        }
    }
}
