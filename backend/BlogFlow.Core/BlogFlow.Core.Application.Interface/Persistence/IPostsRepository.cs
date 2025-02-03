using BlogFlow.Auth.Application.Interface.Persistence;
using BlogFlow.Core.Domain.Entities;

namespace BlogFlow.Core.Application.Interface.Persistence
{
    public interface IPostsRepository : IGenericRepository<Post>
    {
    }
}
