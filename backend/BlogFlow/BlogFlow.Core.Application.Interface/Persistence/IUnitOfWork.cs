namespace BlogFlow.Core.Application.Interface.Persistence
{
    public interface IUnitOfWork : IDisposable
    {
        IUsersRepository Users { get; }
        IBlogsRepository Blogs { get; }
        IPostsRepository Posts { get; }
        IRefreshTokenRepository RefreshTokens { get; }
        IImageRepository Images { get; }
        IFollowerRepository Followers { get; }
        Task<int> Save(CancellationToken cancellationToken);
    }
}
