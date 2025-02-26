using BlogFlow.Core.Domain.Entities;

namespace BlogFlow.Core.Application.Interface.Persistence
{
    public interface IPostsRepository : IGenericRepository<Post>
    {
        Task<IEnumerable<Post>> GetByIdPostAsync(string idPost, CancellationToken cancellationToken = default);
    }
}
