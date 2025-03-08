using BlogFlow.Core.Application.Interface.Persistence;
using BlogFlow.Core.Domain.Entities;
using BlogFlow.Core.Infrastructure.Persistence.Contexts;
using Microsoft.EntityFrameworkCore;

namespace BlogFlow.Core.Infrastructure.Persistence.Repositories
{
    class RefreshTokenRepository : IRefreshTokenRepository
    {
        protected readonly ApplicationDbContext _applicationDbContext;

        public RefreshTokenRepository(ApplicationDbContext applicationDbContext)
        {
            _applicationDbContext = applicationDbContext;
        }

        #region Sync

        public int Count()
        {
            throw new NotImplementedException();
        }

        public bool Delete(string id)
        {
            throw new NotImplementedException();
        }

        public RefreshToken Get(string id)
        {
            throw new NotImplementedException();
        }

        public IEnumerable<RefreshToken> GetAll()
        {
            throw new NotImplementedException();
        }

        public bool Insert(RefreshToken entity)
        {
            throw new NotImplementedException();
        }

        public bool Update(RefreshToken entity)
        {
            throw new NotImplementedException();
        }

        #endregion

        #region Async

        public Task<int> CountAsync(CancellationToken cancellationToken = default)
        {
            throw new NotImplementedException();
        }

        public Task<bool> DeleteAsync(string id)
        {
            throw new NotImplementedException();
        }

        public Task<IEnumerable<RefreshToken>> GetAllAsync(CancellationToken cancellationToken = default)
        {
            throw new NotImplementedException();
        }

        public Task<RefreshToken> GetAsync(string id, CancellationToken cancellationToken = default)
        {
            throw new NotImplementedException();
        }

        public async Task<bool> InsertAsync(RefreshToken entity)
        {
            await _applicationDbContext.AddAsync(entity);
            return await Task.FromResult(true);
        }

        public async Task<bool> UpdateAsync(RefreshToken entity)
        {
            var entityToUpdate = await _applicationDbContext.Set<RefreshToken>().AsNoTracking().SingleOrDefaultAsync(x => x.Token.Equals(entity.Token));

            if (entityToUpdate == null)
            {
                return await Task.FromResult(false);
            }

            entityToUpdate.Token = entity.Token;
            entityToUpdate.JwtId = entity.JwtId;
            entityToUpdate.IsRevoked = entity.IsRevoked;
            entityToUpdate.IsUsed = entity.IsUsed;
            entityToUpdate.ExpiryDate = entity.ExpiryDate;
            entityToUpdate.UserId = entity.UserId;

            _applicationDbContext.Update(entityToUpdate);

            return await Task.FromResult(true);
        }

        public async Task<RefreshToken> GetByTokenAsync(string token, CancellationToken cancellationToken = default)
        {
            return await _applicationDbContext.Set<RefreshToken>().AsNoTracking().SingleOrDefaultAsync(x => x.Token.Equals(token), cancellationToken);
        }

        #endregion
    }
}
