using BlogFlow.Common.Persistence.Contexts;
using BlogFlow.Common.Application.Interface.Persistence;
using BlogFlow.Core.Domain.Entities;

namespace BlogFlow.Common.Persistence.Repositories
{
    public class ContentsRepository : IContentsRepository
    {
        protected readonly ApplicationDbContext _applicationDbContext;

        public ContentsRepository(ApplicationDbContext applicationDbContext)
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

        public Content Get(string id)
        {
            throw new NotImplementedException();
        }

        public IEnumerable<Content> GetAll()
        {
            throw new NotImplementedException();
        }

        public Task<IEnumerable<Content>> GetAllAsync(CancellationToken cancellationToken)
        {
            throw new NotImplementedException();
        }

        public Task<Content> GetAsync(string id, CancellationToken cancellationToken)
        {
            throw new NotImplementedException();
        }

        public bool Insert(Content entity)
        {
            throw new NotImplementedException();
        }

        public Task<bool> InsertAsync(Content entity)
        {
            throw new NotImplementedException();
        }

        public bool Update(Content entity)
        {
            throw new NotImplementedException();
        }

        public Task<bool> UpdateAsync(Content entity)
        {
            throw new NotImplementedException();
        }
    }
}
