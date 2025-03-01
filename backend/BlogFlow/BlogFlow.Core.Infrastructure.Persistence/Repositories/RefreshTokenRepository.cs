using BlogFlow.Core.Application.Interface.Persistence;
using BlogFlow.Core.Domain.Entities;
using BlogFlow.Core.Infrastructure.Persistence.Contexts;

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

        public Task<bool> UpdateAsync(RefreshToken entity)
        {
            throw new NotImplementedException();
        }

        #endregion
    }
}
