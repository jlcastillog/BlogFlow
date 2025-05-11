using BlogFlow.Core.Application.Interface.Persistence;
using BlogFlow.Core.Infrastructure.Persistence.Contexts;

namespace BlogFlow.Core.Infrastructure.Persistence.Repositories
{
    public class UnitOfWork : IUnitOfWork
    {
        public IUsersRepository Users { get; }
        public IBlogsRepository Blogs { get; }
        public IPostsRepository Posts { get; }
        public IRefreshTokenRepository RefreshTokens { get; }
        public IImageRepository Images { get; }

        private readonly ApplicationDbContext _applicaDbContext;

        public UnitOfWork(IUsersRepository users, 
                          IBlogsRepository blogs, 
                          IPostsRepository posts,
                          IRefreshTokenRepository refreshTokens,
                          IImageRepository images,
                          ApplicationDbContext applicationDbContext)
        {
            Users = users;
            Blogs = blogs;
            Posts = posts;
            RefreshTokens = refreshTokens;
            Images = images;
            _applicaDbContext = applicationDbContext;
        }

        public async Task<int> Save(CancellationToken cancellationToken)
        {
            return await _applicaDbContext.SaveChangesAsync(cancellationToken);
        }

        public void Dispose()
        {
            GC.SuppressFinalize(this);
        }
    }
}
