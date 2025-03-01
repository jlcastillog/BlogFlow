namespace BlogFlow.Core.Application.Interface.Persistence
{
    public interface IUnitOfWork : IDisposable
    {
        IUsersRepository Users { get; }
        IBlogsRepository Blogs { get; }
        IPostsRepository Posts { get; }
        IContentsRepository Contents { get; }
        IRefreshTokenRepository RefreshTokens { get; }
        Task<int> Save(CancellationToken cancellationToken);
    }
}
