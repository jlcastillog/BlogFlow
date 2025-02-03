using BlogFlow.Common.Persistence.Contexts;
using BlogFlow.Core.Application.Interface.Persistence;
using BlogFlow.Core.Domain.Entities;

namespace BlogFlow.Common.Persistence.Repositories
{
    public class BlogsRepository : IBlogsRepository
    {
        protected readonly ApplicationDbContext _applicationDbContext;

        public BlogsRepository(ApplicationDbContext applicationDbContext)
        {
            _applicationDbContext = applicationDbContext;
        }

        public int Count()
        {
            throw new NotImplementedException();
        }

        public Task<int> CountAsync(CancellationToken cancellationToken)
        {
            throw new NotImplementedException();
        }

        public bool Delete(string id)
        {
            throw new NotImplementedException();
        }

        public Task<bool> DeleteAsync(string id)
        {
            throw new NotImplementedException();
        }

        public Blog Get(string id)
        {
            throw new NotImplementedException();
        }

        public IEnumerable<Blog> GetAll()
        {
            throw new NotImplementedException();
        }

        public Task<IEnumerable<Blog>> GetAllAsync(CancellationToken cancellationToken)
        {
            throw new NotImplementedException();
        }

        public Task<Blog> GetAsync(string id, CancellationToken cancellationToken)
        {
            throw new NotImplementedException();
        }

        public bool Insert(Blog entity)
        {
            throw new NotImplementedException();
        }

        public Task<bool> InsertAsync(Blog entity)
        {
            throw new NotImplementedException();
        }

        public bool Update(Blog entity)
        {
            throw new NotImplementedException();
        }

        public Task<bool> UpdateAsync(Blog entity)
        {
            throw new NotImplementedException();
        }
    }
}
