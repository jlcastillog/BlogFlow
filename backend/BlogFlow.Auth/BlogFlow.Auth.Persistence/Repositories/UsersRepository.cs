using BlogFlow.Auth.Application.Interface.Persistence;
using BlogFlow.Auth.Domain.Entities;
using BlogFlow.Auth.Persistence.Contexts;
using Microsoft.EntityFrameworkCore;

namespace BlogFlow.Auth.Persistence.Repositories
{
    public class UsersRepository : IUsersRepository
    {
        protected readonly ApplicationDbContext _applicationDbContext;

        public UsersRepository(ApplicationDbContext applicationDbContext)
        {
            _applicationDbContext = applicationDbContext;
        }

        #region Sync methods

        public int Count()
        {
            throw new NotImplementedException();
        }

        public bool Delete(string id)
        {
            throw new NotImplementedException();
        }

        public User Get(string id)
        {
            throw new NotImplementedException();
        }

        public IEnumerable<User> GetAll()
        {
            throw new NotImplementedException();
        }

        public bool Insert(User entity)
        {
            throw new NotImplementedException();
        }

        public bool Update(User entity)
        {
            throw new NotImplementedException();
        }

        #endregion

        #region Async methods

        public async Task<int> CountAsync(CancellationToken cancellationToken)
        {
            return await _applicationDbContext.Users.CountAsync(cancellationToken);
        }

        public async Task<User> Authenticate(string userName, string password, CancellationToken cancellationToken)
        {
            return await _applicationDbContext.Users.SingleOrDefaultAsync(x => x.UserName == userName && x.Password == password, cancellationToken);
        }

        public async Task<bool> DeleteAsync(string id)
        {
            var entity = await _applicationDbContext.Set<User>().AsNoTracking().SingleOrDefaultAsync(x => x.UserId.Equals(int.Parse(id)));

            if (entity == null)
            {
                return await Task.FromResult(false);
            }

            _applicationDbContext.Remove(entity);

            return await Task.FromResult(true);
        }

        public async Task<IEnumerable<User>> GetAllAsync(CancellationToken cancellationToken)
        {
            return await _applicationDbContext.Set<User>().AsNoTracking().ToListAsync(cancellationToken);
        }

        public async Task<User> GetAsync(string id, CancellationToken cancellationToken)
        {
            return await _applicationDbContext.Set<User>().AsNoTracking().SingleOrDefaultAsync(x => x.UserId.Equals(int.Parse(id)), cancellationToken);
        }

        public async Task<bool> InsertAsync(User entity)
        {
            await _applicationDbContext.AddAsync(entity);
            return await Task.FromResult(true);
        }

        public async Task<bool> UpdateAsync(User entity)
        {
            var entityToUpdate = await _applicationDbContext.Set<User>().AsNoTracking().SingleOrDefaultAsync(x => x.UserId.Equals(entity.UserId));

            if (entityToUpdate == null)
            {
                return await Task.FromResult(false);
            }

            entityToUpdate.FirstName = entity.FirstName;
            entityToUpdate.LastName = entity.LastName;
            entityToUpdate.UserName = entity.UserName;

            _applicationDbContext.Update(entityToUpdate);

            return await Task.FromResult(true);
        }

        #endregion
    }
}
