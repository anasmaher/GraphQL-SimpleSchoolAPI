using System.Linq.Expressions;
using GQLDomain.Entities;

namespace GQLApp.Interfaces
{
    public interface IRepository<T> where T : class
    {
        Task<Response> GetAllAsync(Expression<Func<T, bool>> filter = null);

        Task<Response> GetSingleAsync(Expression<Func<T, bool>> filter);

        Task<Response> AddAsync(T Entity);

        Task<Response> RemoveAsync(Expression<Func<T, bool>> filter);
    }
}
