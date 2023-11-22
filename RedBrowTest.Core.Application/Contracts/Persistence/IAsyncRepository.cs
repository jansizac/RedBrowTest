using RedBrowTest.Core.Application.Models.Pagination;
using RedBrowTest.Core.Domain.Common;
using System.Linq.Expressions;

namespace RedBrowTest.Core.Application.Contracts.Persistence
{
    public interface IAsyncRepository<T> where T : class, ICommonEntity
    {
        Task<bool> ExistsAsync(string id);

        Task<bool> ExistsAsync(params object?[]? keyValuyes);

        Task<IReadOnlyList<T>> GetAllAsync(bool includeErased = false);

        Task<IReadOnlyList<T>> GetAsync(Expression<Func<T, bool>> predicate);

        Task<IReadOnlyList<T>> GetAsync(Expression<Func<T, bool>> predicate = null,
                                        Func<IQueryable<T>, IOrderedQueryable<T>> orderBy = null,
                                        string includeString = null,
                                        bool disableTracking = true);

        Task<IReadOnlyList<T>> GetAsync(Expression<Func<T, bool>> predicate = null,
                                        Func<IQueryable<T>, IOrderedQueryable<T>> orderBy = null,
                                        List<Expression<Func<T, object>>> includes = null,
                                        bool disableTracking = true);

        Task<T?> GetFirstOrDefaultAsync(Expression<Func<T, bool>> predicate = null,
                                       Func<IQueryable<T>, IOrderedQueryable<T>> orderBy = null,
                                       List<Expression<Func<T, object>>> includes = null,
                                       bool disableTracking = true);

        Task<T?> GetByIdAsync(string id);

        Task<T?> GetByIdAsync(params object?[]? keyValuyes);

        Task<PagedResult<T>> GetPagedAsync(int page,
                                           int limit,
                                           Expression<Func<T, bool>> predicate = null,
                                           Func<IQueryable<T>, IOrderedQueryable<T>> orderBy = null,
                                           List<Expression<Func<T, object>>> includes = null);

        Task<T> AddAsync(T entity);

        Task<IEnumerable<T>> AddRangeAsync(IEnumerable<T> entities);

        Task<T> UpdateAsync(T entity);

        Task DeleteAsync(T entity);

        Task<T> SoftDeleteAsync(T entity);
    }
}
