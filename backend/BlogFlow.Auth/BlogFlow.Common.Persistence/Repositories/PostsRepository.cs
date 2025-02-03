using BlogFlow.Common.Persistence.Contexts;
using BlogFlow.Core.Application.Interface.Persistence;
using BlogFlow.Core.Domain.Entities;

namespace BlogFlow.Common.Persistence.Repositories
{
    public class PostsRepository : IPostsRepository
    {
        protected readonly ApplicationDbContext _applicationDbContext;

        public PostsRepository(ApplicationDbContext applicationDbContext)
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

        public Post Get(string id)
        {
            throw new NotImplementedException();
        }

        public IEnumerable<Post> GetAll()
        {
            throw new NotImplementedException();
        }

        public Task<IEnumerable<Post>> GetAllAsync(CancellationToken cancellationToken)
        {
            throw new NotImplementedException();
        }

        public Task<Post> GetAsync(string id, CancellationToken cancellationToken)
        {
            throw new NotImplementedException();
        }

        public bool Insert(Post entity)
        {
            throw new NotImplementedException();
        }

        public Task<bool> InsertAsync(Post entity)
        {
            throw new NotImplementedException();
        }

        public bool Update(Post entity)
        {
            throw new NotImplementedException();
        }

        public Task<bool> UpdateAsync(Post entity)
        {
            throw new NotImplementedException();
        }
    }
}
