using System.Threading;

namespace BlogFlow.Auth.Application.Interface.Persistence
{
    public interface IGenericRepository <T> where T : class
    {
        #region Sync methods

        bool Insert(T entity);
        bool Update(T entity);
        bool Delete(string id);
        T Get(string id);
        IEnumerable<T> GetAll();
        int Count();

        #endregion

        #region Async methods

        Task<bool> InsertAsync(T entity);
        Task<bool> UpdateAsync(T entity);
        Task<bool> DeleteAsync(string id);
        Task<T> GetAsync(string id, CancellationToken cancellationToken);
        Task<IEnumerable<T>> GetAllAsync(CancellationToken cancellationToken);
        Task<int> CountAsync(CancellationToken cancellationToken);

        #endregion
    }
}
