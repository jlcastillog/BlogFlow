using BlogFlow.Core.Domain.Entities;

namespace BlogFlow.Core.Application.Interface.Persistence
{
    public interface IUsersRepository : IGenericRepository<User>
    {
        Task<User> Authenticate(string userName, string password, CancellationToken cancellationToken);
    }
}
