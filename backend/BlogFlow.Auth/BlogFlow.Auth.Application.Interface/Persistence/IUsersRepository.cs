using BlogFlow.Auth.Domain.Entities;

namespace BlogFlow.Auth.Application.Interface.Persistence
{
    public interface IUsersRepository : IGenericRepository<User>
    {
        Task<User> Authenticate(string userName, string password, CancellationToken cancellationToken);
    }
}
