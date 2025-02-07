using BlogFlow.Core.Domain.Entities;
using BlogFlow.Core.Infrastructure.Persistence.Contexts;
using BlogFlow.Core.Application.Interface.Persistence;
using BlogFlow.Core.Domain.Entities;
using Microsoft.EntityFrameworkCore;

namespace BlogFlow.Core.Infrastructure.Persistence.Repositories
{
    public class BlogsRepository : IBlogsRepository
    {
        protected readonly ApplicationDbContext _applicationDbContext;

        public BlogsRepository(ApplicationDbContext applicationDbContext)
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

        public Blog Get(string id)
        {
            throw new NotImplementedException();
        }

        public IEnumerable<Blog> GetAll()
        {
            throw new NotImplementedException();
        }

        public bool Insert(Blog entity)
        {
            throw new NotImplementedException();
        }

        public bool Update(Blog entity)
        {
            throw new NotImplementedException();
        }

        #endregion

        #region ASync 

        public async Task<int> CountAsync(CancellationToken cancellationToken)
        {
            return await _applicationDbContext.Blogs.CountAsync(cancellationToken);
        }


        public async Task<bool> DeleteAsync(string id)
        {
            var entity = await _applicationDbContext.Set<Blog>().AsNoTracking().SingleOrDefaultAsync(x => x.Id.Equals(int.Parse(id)));

            if (entity == null)
            {
                return await Task.FromResult(false);
            }

            _applicationDbContext.Remove(entity);

            return await Task.FromResult(true);
        }


        public async Task<IEnumerable<Blog>> GetAllAsync(CancellationToken cancellationToken)
        {
            return await _applicationDbContext.Set<Blog>().AsNoTracking().ToListAsync(cancellationToken);
        }

        public async Task<Blog> GetAsync(string id, CancellationToken cancellationToken)
        {
            return await _applicationDbContext.Set<Blog>().AsNoTracking().SingleOrDefaultAsync(x => x.Id.Equals(int.Parse(id)), cancellationToken);
        }

        public async Task<bool> InsertAsync(Blog entity)
        {
            await _applicationDbContext.AddAsync(entity);
            return await Task.FromResult(true);
        }

        public async Task<bool> UpdateAsync(Blog entity)
        {
            var entityToUpdate = await _applicationDbContext.Set<Blog>().AsNoTracking().SingleOrDefaultAsync(x => x.Id.Equals(entity.Id));

            if (entityToUpdate == null)
            {
                return await Task.FromResult(false);
            }

            entityToUpdate.Title = entity.Title;
            entityToUpdate.Description = entity.Description;
            entityToUpdate.Category = entity.Category;
            entityToUpdate.Image = entity.Image;

            _applicationDbContext.Update(entityToUpdate);

            return await Task.FromResult(true);
        }

        #endregion
    }
}
