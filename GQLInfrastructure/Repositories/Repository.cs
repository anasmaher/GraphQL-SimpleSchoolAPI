using GQLApp.Interfaces;
using GQLDomain.Database;
using GQLDomain.Entities;
using Microsoft.EntityFrameworkCore;
using System.Linq.Expressions;
using System.Net;

namespace GQLInfrastructure.Repositories
{
    public class Repository<T> : IRepository<T> where T : class
    {
        private readonly IDbContextFactory<AppDbContext> _contextFactory;

        public Repository(IDbContextFactory<AppDbContext> contextFactory)
        {
            _contextFactory = contextFactory;
        }

        public async Task<Response> AddAsync(T entity)
        {
            try
            {
                await using var context = _contextFactory.CreateDbContext();
                await context.Set<T>().AddAsync(entity);
                await context.SaveChangesAsync();

                return new Response(HttpStatusCode.OK, true, entity, Message: "Added");
            }
            catch (Exception ex)
            {
                return new Response(HttpStatusCode.BadRequest, false, ex.Message, "Not added");
            }
        }

        public async Task<Response> GetAllAsync(Expression<Func<T, bool>>? filter = null)
        {
            try
            {
                await using var context = _contextFactory.CreateDbContext();
                IQueryable<T> query = context.Set<T>();

                if (filter is not null)
                {
                    query = query.Where(filter);
                }

                var res = await query.ToListAsync();
                return new Response(HttpStatusCode.OK, true, res, Message: "All returned");
            }
            catch (Exception ex)
            {
                return new Response(HttpStatusCode.BadRequest, false, Error: ex.Message, Message: "All not returned");
            }
        }

        public async Task<Response> GetSingleAsync(Expression<Func<T, bool>> filter)
        {
            try
            {
                await using var context = _contextFactory.CreateDbContext();
                IQueryable<T> query = context.Set<T>();

                var res = await query.FirstOrDefaultAsync(filter);
                return new Response(HttpStatusCode.OK, true, res, Message: "Found");
            }
            catch (Exception ex)
            {
                return new Response(HttpStatusCode.NotFound, false, Error: ex.Message, Message: "Not Found");
            }
        }

        public async Task<Response> UpdateAsync(Expression<Func<T, bool>> filter, T updatedEntity)
        {
            try
            {
                await using var context = _contextFactory.CreateDbContext();
                var entity = await GetSingleAsync(filter);

                context.Entry(entity).CurrentValues.SetValues(updatedEntity);
                await context.SaveChangesAsync();

                return new Response(HttpStatusCode.OK, true, entity, Message: "Updated");
            }
            catch (Exception ex)
            {
                return new Response(HttpStatusCode.NotFound, false, Error: ex.Message, Message: "Not Updated");
            }
        }

        public async Task<Response> RemoveAsync(Expression<Func<T, bool>> filter)
        {
            try
            {
                await using var context = _contextFactory.CreateDbContext();
                var entity = await GetSingleAsync(filter);

                context.Set<T>().Remove((T)entity.Result);
                await context.SaveChangesAsync();

                return new Response(HttpStatusCode.OK, true, Message: "Removed");
            }
            catch (Exception ex)
            {
                return new Response(HttpStatusCode.NotFound, false, Error: ex.Message, Message: "Not removed");
            }
        }
    }
}
