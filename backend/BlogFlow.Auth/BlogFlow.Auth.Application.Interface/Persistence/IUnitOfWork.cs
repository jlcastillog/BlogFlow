namespace BlogFlow.Auth.Application.Interface.Persistence
{
    public interface IUnitOfWork : IDisposable
    {
        IUsersRepository Users { get; }
    }
}
