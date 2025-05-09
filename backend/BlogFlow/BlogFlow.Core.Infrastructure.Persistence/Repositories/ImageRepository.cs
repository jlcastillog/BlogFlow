using BlogFlow.Core.Application.Interface.Persistence;
using BlogFlow.Core.Domain.Entities;
using BlogFlow.Core.Infrastructure.Persistence.Contexts;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BlogFlow.Core.Infrastructure.Persistence.Repositories
{
    public class ImageRepository : IImageRepository
    {
        protected readonly ApplicationDbContext _applicationDbContext;

        public ImageRepository(ApplicationDbContext applicationDbContext)
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

        public Image Get(string id)
        {
            throw new NotImplementedException();
        }

        public IEnumerable<Image> GetAll()
        {
            throw new NotImplementedException();
        }

        public bool Insert(Image entity)
        {
            throw new NotImplementedException();
        }

        public bool Update(Image entity)
        {
            throw new NotImplementedException();
        }

        #endregion

        #region Async

        public async Task<int> CountAsync(CancellationToken cancellationToken = default)
        {
            return await _applicationDbContext.Images.CountAsync(cancellationToken);
        }

        public async Task<bool> DeleteAsync(string id)
        {
            var entity = await _applicationDbContext.Set<Image>().AsNoTracking().SingleOrDefaultAsync(x => x.Id.Equals(int.Parse(id)));

            if (entity == null)
            {
                return await Task.FromResult(false);
            }

            _applicationDbContext.Remove(entity);

            return await Task.FromResult(true);
        }

        public async Task<IEnumerable<Image>> GetAllAsync(CancellationToken cancellationToken = default)
        {
            return await _applicationDbContext.Set<Image>().AsNoTracking().ToListAsync(cancellationToken);
        }

        public async Task<Image> GetAsync(string id, CancellationToken cancellationToken = default)
        {
            return await _applicationDbContext.Set<Image>().AsNoTracking().SingleOrDefaultAsync(x => x.Id.Equals(int.Parse(id)), cancellationToken);
        }

        public async Task<bool> InsertAsync(Image entity)
        {
            await _applicationDbContext.AddAsync(entity);
            return await Task.FromResult(true);
        }

        public async Task<bool> UpdateAsync(Image entity)
        {
            var entityToUpdate = await _applicationDbContext.Set<Image>().AsNoTracking().SingleOrDefaultAsync(x => x.Id.Equals(entity.Id));

            if (entityToUpdate == null)
            {
                return await Task.FromResult(false);
            }

            entityToUpdate.Url = entity.Url;
            entityToUpdate.PublicId = entity.PublicId;

            _applicationDbContext.Update(entityToUpdate);

            return await Task.FromResult(true);
        }

        #endregion
    }
}
