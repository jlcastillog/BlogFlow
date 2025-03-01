﻿using BlogFlow.Core.Application.Interface.Persistence;
using BlogFlow.Core.Application.Interface.Persistence;
using BlogFlow.Core.Infrastructure.Persistence.Contexts;

namespace BlogFlow.Core.Infrastructure.Persistence.Repositories
{
    public class UnitOfWork : IUnitOfWork
    {
        public IUsersRepository Users { get; }
        public IBlogsRepository Blogs { get; }
        public IPostsRepository Posts { get; }
        public IContentsRepository Contents { get; }
        private readonly ApplicationDbContext _applicaDbContext;

        public UnitOfWork(IUsersRepository users, IBlogsRepository blogs, IPostsRepository posts, IContentsRepository contents, ApplicationDbContext applicationDbContext)
        {
            Users = users;
            Blogs = blogs;
            Posts = posts;
            Contents = contents;
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
