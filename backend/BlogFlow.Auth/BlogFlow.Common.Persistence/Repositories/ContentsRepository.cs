using BlogFlow.Common.Persistence.Contexts;
using BlogFlow.Common.Application.Interface.Persistence;
using BlogFlow.Core.Domain.Entities;
using Microsoft.EntityFrameworkCore;

namespace BlogFlow.Common.Persistence.Repositories
{
    public class ContentsRepository : IContentsRepository
    {
        protected readonly ApplicationDbContext _applicationDbContext;

        public ContentsRepository(ApplicationDbContext applicationDbContext)
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

        public Content Get(string id)
        {
            throw new NotImplementedException();
        }

        public IEnumerable<Content> GetAll()
        {
            throw new NotImplementedException();
        }

        public bool Insert(Content entity)
        {
            throw new NotImplementedException();
        }

        public bool Update(Content entity)
        {
            throw new NotImplementedException();
        }

        #endregion

        #region ASync 

        public async Task<int> CountAsync(CancellationToken cancellationToken)
        {
            return await _applicationDbContext.Contents.CountAsync(cancellationToken);
        }

        public async Task<bool> DeleteAsync(string id)
        {
            var entity = await _applicationDbContext.Set<Content>().AsNoTracking().SingleOrDefaultAsync(x => x.Id.Equals(int.Parse(id)));

            if (entity == null)
            {
                return await Task.FromResult(false);
            }

            _applicationDbContext.Remove(entity);

            return await Task.FromResult(true);
        }


        public async Task<IEnumerable<Content>> GetAllAsync(CancellationToken cancellationToken)
        {
            return await _applicationDbContext.Set<Content>().AsNoTracking().ToListAsync(cancellationToken);
        }

        public async Task<Content> GetAsync(string id, CancellationToken cancellationToken)
        {
            return await _applicationDbContext.Set<Content>().AsNoTracking().SingleOrDefaultAsync(x => x.Id.Equals(int.Parse(id)), cancellationToken);
        }

        public async Task<bool> InsertAsync(Content entity)
        {
            await _applicationDbContext.AddAsync(entity);
            return await Task.FromResult(true);
        }

        public async Task<bool> UpdateAsync(Content entity)
        {
            var entityToUpdate = await _applicationDbContext.Set<Content>().AsNoTracking().SingleOrDefaultAsync(x => x.Id.Equals(entity.Id));

            if (entityToUpdate == null)
            {
                return await Task.FromResult(false);
            }

            entityToUpdate.TextContent = entity.TextContent;

            _applicationDbContext.Update(entityToUpdate);

            return await Task.FromResult(true);
        }

        #endregion
    }
}
