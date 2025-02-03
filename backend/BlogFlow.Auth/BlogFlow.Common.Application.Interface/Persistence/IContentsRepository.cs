using BlogFlow.Common.Application.Interface.Persistence;
using BlogFlow.Core.Domain.Entities;

namespace BlogFlow.Common.Application.Interface.Persistence
{
    public interface IContentsRepository : IGenericRepository<Content>
    {
    }
}
