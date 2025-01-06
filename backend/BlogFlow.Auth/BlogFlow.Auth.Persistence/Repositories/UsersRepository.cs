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

        public IEnumerable<User> GetAllWithPagination(int pageNumber, int pageSize)
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

        public Task<int> CountAsync()
        {
            throw new NotImplementedException();
        }

        public async Task<User> Authenticate(string userName, string password, CancellationToken cancellationToken)
        {
            return await _applicationDbContext.Users.SingleOrDefaultAsync(x => x.UserName == userName && x.Password == password, cancellationToken);
        }

        public Task<bool> DeleteAsync(string id)
        {
            throw new NotImplementedException();
        }

        public Task<IEnumerable<User>> GetAllAsync()
        {
            throw new NotImplementedException();
        }

        public Task<IEnumerable<User>> GetAllWithPaginationAsync(int pageNumber, int pageSize)
        {
            throw new NotImplementedException();
        }

        public Task<User> GetAsync(string id)
        {
            throw new NotImplementedException();
        }

        public Task<bool> InsertAsync(User entity)
        {
            throw new NotImplementedException();
        }


        public Task<bool> UpdateAsync(User entity)
        {
            throw new NotImplementedException();
        }

        #endregion
    }
}
