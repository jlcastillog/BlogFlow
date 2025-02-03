using BlogFlow.Common.Persistence.Contexts;
using BlogFlow.Common.Application.Interface.Persistence;
using BlogFlow.Core.Domain.Entities;
using Microsoft.EntityFrameworkCore;

namespace BlogFlow.Common.Persistence.Repositories
{
    public class PostsRepository : IPostsRepository
    {
        protected readonly ApplicationDbContext _applicationDbContext;

        public PostsRepository(ApplicationDbContext applicationDbContext)
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

        public Post Get(string id)
        {
            throw new NotImplementedException();
        }

        public IEnumerable<Post> GetAll()
        {
            throw new NotImplementedException();
        }

        public bool Insert(Post entity)
        {
            throw new NotImplementedException();
        }

        public bool Update(Post entity)
        {
            throw new NotImplementedException();
        }

        #endregion

        #region ASync 

        public async Task<int> CountAsync(CancellationToken cancellationToken)
        {
            return await _applicationDbContext.Posts.CountAsync(cancellationToken);
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


        public async Task<IEnumerable<Post>> GetAllAsync(CancellationToken cancellationToken)
        {
            return await _applicationDbContext.Set<Post>().AsNoTracking().ToListAsync(cancellationToken);
        }

        public async Task<Post> GetAsync(string id, CancellationToken cancellationToken)
        {
            return await _applicationDbContext.Set<Post>().AsNoTracking().SingleOrDefaultAsync(x => x.Id.Equals(int.Parse(id)), cancellationToken);
        }

        public async Task<bool> InsertAsync(Post entity)
        {
            await _applicationDbContext.AddAsync(entity);
            return await Task.FromResult(true);
        }

        public async Task<bool> UpdateAsync(Post entity)
        {
            var entityToUpdate = await _applicationDbContext.Set<Post>().AsNoTracking().SingleOrDefaultAsync(x => x.Id.Equals(entity.Id));

            if (entityToUpdate == null)
            {
                return await Task.FromResult(false);
            }

            entityToUpdate.Title = entity.Title;
            entityToUpdate.Description = entity.Description;

            _applicationDbContext.Update(entityToUpdate);

            return await Task.FromResult(true);
        }

        #endregion
    }
}
