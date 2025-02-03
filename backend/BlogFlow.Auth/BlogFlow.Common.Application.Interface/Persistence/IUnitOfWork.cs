namespace BlogFlow.Common.Application.Interface.Persistence
{
    public interface IUnitOfWork : IDisposable
    {
        IUsersRepository Users { get; }
        IBlogsRepository Blogs { get; }
        IPostsRepository Posts { get; }
        IContentsRepository Contents { get; }
        Task<int> Save(CancellationToken cancellationToken);
    }
}
