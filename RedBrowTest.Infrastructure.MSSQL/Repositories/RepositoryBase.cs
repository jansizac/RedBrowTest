using Microsoft.EntityFrameworkCore;
using RedBrowTest.Core.Application.Contracts.Persistence;
using RedBrowTest.Core.Application.Models.Pagination;
using RedBrowTest.Core.Domain.Common;
using RedBrowTest.Infrastructure.MSSQL.Persistence;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace RedBrowTest.Infrastructure.MSSQL.Repositories
{
    public class RepositoryBase<T> : IAsyncRepository<T> where T : class, ICommonEntity
    {
        protected readonly RedBrowTestContext _context;

        public RepositoryBase(RedBrowTestContext context)
        {
            _context = context;
        }

        public async Task<bool> ExistsAsync(string id)
        {
            var entity = await _context.Set<T>().FindAsync(id);
            return entity == null ? false : true;
        }

        public async Task<bool> ExistsAsync(params object?[]? keyValuyes)
        {
            var entity = await _context.Set<T>().FindAsync(keyValuyes);
            return entity == null ? false : true;
        }

        public async Task<T> AddAsync(T entity)
        {
            _context.Set<T>().Add(entity);
            await _context.SaveChangesAsync();
            return entity;
        }

        public async Task<IEnumerable<T>> AddRangeAsync(IEnumerable<T> entities)
        {
            await _context.Set<T>().AddRangeAsync(entities);
            await _context.SaveChangesAsync();
            return entities.ToList();
        }

        public async Task DeleteAsync(T entity)
        {
            _context.Set<T>().Remove(entity);
            await _context.SaveChangesAsync();
        }

        public async Task<IReadOnlyList<T>> GetAllAsync(bool includeErased = false)
        {
            if (!includeErased)
            {
                return await _context.Set<T>().Where(x => x.DeletedAt == null).ToListAsync();
            }

            return await _context.Set<T>().ToListAsync();
        }

        public async Task<IReadOnlyList<T>> GetAsync(Expression<Func<T, bool>> predicate)
        {
            return await _context.Set<T>().Where(predicate).ToListAsync();
        }

        public async Task<IReadOnlyList<T>> GetAsync(Expression<Func<T, bool>> predicate = null,
                                                     Func<IQueryable<T>, IOrderedQueryable<T>> orderBy = null,
                                                     string includeString = null,
                                                     bool disableTracking = true)
        {
            IQueryable<T> query = _context.Set<T>();

            if (disableTracking)
                query = query.AsNoTracking();

            //            if (!string.IsNullOrEmpty(includeString))
            if (!string.IsNullOrWhiteSpace(includeString))
                query = query.Include(includeString);

            if (predicate != null)
                query = query.Where(predicate);

            if (orderBy != null)
                return await orderBy(query).ToListAsync();

            return await query.ToListAsync();
        }

        public async Task<IReadOnlyList<T>> GetAsync(Expression<Func<T, bool>> predicate = null,
                                                     Func<IQueryable<T>, IOrderedQueryable<T>> orderBy = null,
                                                     List<Expression<Func<T, object>>> includes = null,
                                                     bool disableTracking = true)
        {
            IQueryable<T> query = _context.Set<T>();

            if (disableTracking)
                query = query.AsNoTracking();

            if (includes != null)
                query = includes.Aggregate(query, (current, include) => current.Include(include));

            if (predicate != null)
                query = query.Where(predicate);

            if (orderBy != null)
                return await orderBy(query).ToListAsync();

            return await query.ToListAsync();
        }

        public async Task<T?> GetFirstOrDefaultAsync(Expression<Func<T, bool>> predicate = null, Func<IQueryable<T>, IOrderedQueryable<T>> orderBy = null, List<Expression<Func<T, object>>> includes = null, bool disableTracking = true)
        {
            IQueryable<T> query = _context.Set<T>();

            if (disableTracking)
                query = query.AsNoTracking();

            if (includes != null)
                query = includes.Aggregate(query, (current, include) => current.Include(include));

            if (predicate != null)
                query = query.Where(predicate);

            if (orderBy != null)
                return await orderBy(query).FirstOrDefaultAsync();

            return await query.FirstOrDefaultAsync();
        }

        public async Task<T?> GetByIdAsync(string id)
        {
            return await _context.Set<T>().FindAsync(id);
        }

        public async Task<T?> GetByIdAsync(params object?[]? keyValuyes)
        {
            return await _context.Set<T>().FindAsync(keyValuyes);
        }

        public async Task<PagedResult<T>> GetPagedAsync(int page,
                                                        int limit,
                                                        Expression<Func<T, bool>> predicate = null,
                                                        Func<IQueryable<T>, IOrderedQueryable<T>> orderBy = null,
                                                        List<Expression<Func<T, object>>> includes = null)
        {
            IQueryable<T> query = _context.Set<T>();

            if (predicate != null)
            {
                query = query.Where(predicate);
            }

            foreach (var include in includes ?? new List<Expression<Func<T, object>>>())
            {
                query = query.Include(include);
            }

            var totalItems = await query.CountAsync();

            if (orderBy != null)
            {
                query = orderBy(query);
            }

            var items = await query.Skip((page - 1) * limit)
                                   .Take(limit)
                                   .ToListAsync();

            return new PagedResult<T>
            {
                Items = items,
                Meta = new GenericPaginationResponse
                {
                    TotalItems = totalItems,
                    ItemCount = items.Count,
                    ItemsPerPage = limit,
                    TotalPages = (int)Math.Ceiling((double)totalItems / limit),
                    CurrentPage = page
                }
            };
        }

        public async Task<T> SoftDeleteAsync(T entity)
        {
            entity.DeletedAt = DateTime.UtcNow;
            _context.Entry(entity).State = EntityState.Modified;
            await _context.SaveChangesAsync();
            return entity;
        }

        public async Task<T> UpdateAsync(T entity)
        {
            entity.UpdatedAt = DateTime.UtcNow;
            _context.Entry(entity).State = EntityState.Modified;
            await _context.SaveChangesAsync();
            return entity;
        }
    }
}
