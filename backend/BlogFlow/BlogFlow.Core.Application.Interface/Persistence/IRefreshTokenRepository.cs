using BlogFlow.Core.Domain.Entities;

namespace BlogFlow.Core.Application.Interface.Persistence
{
    public interface IRefreshTokenRepository : IGenericRepository<RefreshToken>
    {
        Task<RefreshToken> GetByTokenAsync(string token, CancellationToken cancellationToken = default);
    }
}
