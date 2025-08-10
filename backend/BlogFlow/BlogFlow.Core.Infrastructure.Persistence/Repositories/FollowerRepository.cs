using BlogFlow.Core.Application.Interface.Persistence;
using BlogFlow.Core.Domain.Entities;
using BlogFlow.Core.Infrastructure.Persistence.Contexts;
using Microsoft.EntityFrameworkCore;

namespace BlogFlow.Core.Infrastructure.Persistence.Repositories
{
    public class FollowerRepository : IFollowerRepository
    {
        protected readonly ApplicationDbContext _applicationDbContext;

        public FollowerRepository(ApplicationDbContext applicationDbContext)
        {
            _applicationDbContext = applicationDbContext;
        }

        #region Sync

        public int Count()
        {
            return _applicationDbContext.Followers.Count();
        }

        public bool Delete(string id)
        {
            if (!int.TryParse(id, out int followerId))
                return false;

            var follower = _applicationDbContext.Followers.Find(followerId);
            if (follower == null)
                return false;

            _applicationDbContext.Followers.Remove(follower);
            return _applicationDbContext.SaveChanges() > 0;
        }

        public Follower Get(string id)
        {
            if (!int.TryParse(id, out int followerId))
                return null;

            return _applicationDbContext.Followers
                    .Include(f => f.User)
                    .Include(f => f.Blog)
                    .FirstOrDefault(f => f.Id == followerId)!;
        }

        public IEnumerable<Follower> GetAll()
        {
            return _applicationDbContext.Followers
                    .Include(f => f.User)
                    .Include(f => f.Blog)
                    .ToList();
        }

        public bool Insert(Follower entity)
        {
            _applicationDbContext.Followers.Add(entity);
            return _applicationDbContext.SaveChanges() > 0;
        }

        public bool Update(Follower entity)
        {
            var existing = _applicationDbContext.Followers.Find(entity.Id);
            if (existing == null)
                return false;

            _applicationDbContext.Entry(existing).CurrentValues.SetValues(entity);
            return _applicationDbContext.SaveChanges() > 0;
        }

        #endregion

        #region Async

        public async Task<int> CountAsync(CancellationToken cancellationToken = default)
        {
            return await _applicationDbContext.Followers.CountAsync(cancellationToken);
        }

        public async Task<bool> DeleteAsync(string id)
        {
            if (!int.TryParse(id, out int followerId))
                return false;

            var follower = await _applicationDbContext.Followers.FindAsync(new object[] { followerId }, cancellationToken: default);
            if (follower == null)
                return false;

            _applicationDbContext.Followers.Remove(follower);
            return await _applicationDbContext.SaveChangesAsync() > 0;
        }

        public async Task<IEnumerable<Follower>> GetAllAsync(CancellationToken cancellationToken = default)
        {
            return await _applicationDbContext.Followers
                .Include(f => f.User)
                .Include(f => f.Blog)
                .ToListAsync(cancellationToken);
        }

        public async Task<Follower> GetAsync(string id, CancellationToken cancellationToken = default)
        {
            if (string.IsNullOrWhiteSpace(id))
                throw new ArgumentNullException(nameof(id), "El id no puede ser nulo o vacío.");

            if (!int.TryParse(id, out int followerId))
                throw new ArgumentException("El id debe ser un número entero válido.", nameof(id));

            var follower = await _applicationDbContext.Followers
                .Include(f => f.User)
                .Include(f => f.Blog)
                .FirstOrDefaultAsync(f => f.Id == followerId, cancellationToken);

            if (follower == null)
                throw new KeyNotFoundException($"No se encontró ningún seguidor con el id {id}.");

            return follower;
        }

        public async Task<bool> InsertAsync(Follower entity)
        {
            await _applicationDbContext.Followers.AddAsync(entity);
            return await _applicationDbContext.SaveChangesAsync() > 0;
        }

        public async Task<bool> UpdateAsync(Follower entity)
        {
            var existing = await _applicationDbContext.Followers.FindAsync(new object[] { entity.Id }, cancellationToken: default);
            if (existing == null)
                return false;

            _applicationDbContext.Entry(existing).CurrentValues.SetValues(entity);
            return await _applicationDbContext.SaveChangesAsync() > 0;
        }

        #endregion
    }
}
