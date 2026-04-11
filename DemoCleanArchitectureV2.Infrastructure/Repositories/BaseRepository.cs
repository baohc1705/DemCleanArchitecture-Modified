using DemoCleanArchitectureV2.Domain.Interfaces;
using DemoCleanArchitectureV2.Infrastructure.Data;
using Microsoft.EntityFrameworkCore;
using System.Linq.Expressions;

namespace DemoCleanArchitectureV2.Infrastructure.Repositories
{
    public class BaseRepository<T, TKey> : IBaseRepository<T, TKey> where T : class
    {
        protected readonly AppDbContext context;
        protected readonly DbSet<T> dbSet;
        public BaseRepository(AppDbContext context)
        {
            this.context = context;
            dbSet = context.Set<T>();
        }

        public async Task AddAsync(T entity)
        {
            await dbSet.AddAsync(entity);
            context.SaveChanges();
        }

        public async Task AddRangeAsync(IEnumerable<T> entities)
        {
            await dbSet.AddRangeAsync(entities);
            context.SaveChanges();
        }

        public async Task<bool> ExistsAsync(Expression<Func<T, bool>> predicate)
        {
            return await dbSet.AsNoTracking()
                .AnyAsync(predicate);
        }

        public async Task<IEnumerable<T>> FindAsync(Expression<Func<T, bool>> predicate)
        {
            return await dbSet.AsNoTracking()
                .Where(predicate)
                .ToListAsync();
        }


        public async Task<IEnumerable<T>> GetAllAsync()
        {
            return await dbSet.AsNoTracking().ToListAsync();
        }

        public async Task<IQueryable<T>> GetAllQueryAsync()
        {
            return dbSet;
        }

        public async Task<T> GetByIdAsync(TKey key)
        {
            return await dbSet.FindAsync(key);
        }

        public async Task<T?> GetFirstOrDefaultAsync(Expression<Func<T, bool>> predicate)
        {
            return await dbSet.AsNoTracking().FirstOrDefaultAsync(predicate);
        }

        public async Task<IEnumerable<T>> GetListAsync(Expression<Func<T, bool>> predicate)
        {
            return await dbSet.Where(predicate)
                .AsNoTracking()
                .ToListAsync();
        }

     
        public async Task<bool> Remove(TKey id)
        {
            T entity = await GetByIdAsync(id);
            if (entity == null)
                return false;
            dbSet.Remove(entity);
            context.SaveChanges();
            return true;
        }

        public void Remove(T entity)
        {
            dbSet.Remove(entity);
            context.SaveChanges();
        }

        public void RemoveRange(IEnumerable<T> entities)
        {
            dbSet.RemoveRange(entities);
            context.SaveChanges();
        }

        public void Update(T entity)
        {
            dbSet.Attach(entity);
            context.Entry(entity).State = EntityState.Modified;
            context.SaveChanges();
        }

        public void UpdateRange(IEnumerable<T> entities)
        {
           dbSet.AttachRange(entities);
            foreach (T entity in entities)
                context.Entry(entity).State = EntityState.Modified;
            context.SaveChanges();
        }
    }
}
