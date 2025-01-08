namespace BlogFlow.Auth.Application.Interface.Persistence
{
    public interface IUnitOfWork : IDisposable
    {
        IUsersRepository Users { get; }
        Task<int> Save(CancellationToken cancellationToken);
    }
}
