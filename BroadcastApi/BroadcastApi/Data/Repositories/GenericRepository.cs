using BroadcastApi.Data.Entities;
using Microsoft.EntityFrameworkCore;
using System.Linq;
using System.Threading.Tasks;

namespace BroadcastApi.Data.Repositories
{
    public class GenericRepository<T> : IGenericRepository<T> where T : class, IEntity
    {
        readonly ApplicationDbContext context;

        public GenericRepository(ApplicationDbContext context)
        {
            this.context = context;
        }

        public IQueryable<T> GetAll()
        {
            return this.context.Set<T>().Where(e => !e.IsDeleted).AsNoTracking();
        }

        public IQueryable<T> GetAllDeleted()
        {
            return this.context.Set<T>().Where(e => e.IsDeleted).AsNoTracking();
        }

        /// <summary>
        /// Returns the entity requested by its unique Identifier.
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public async Task<T> GetByIdAsync(int id)
        {
            return await this.context.Set<T>()
                .AsNoTracking()
                .FirstOrDefaultAsync(e => e.Id == id && !e.IsDeleted);
        }
        public async Task<T> GetDeletedByIdAsync(int id)
        {
            return await this.context.Set<T>()
                .AsNoTracking()
                .FirstOrDefaultAsync(e => e.Id == id && e.IsDeleted);
        }
        public async Task<T> CreateAsync(T entity)
        {
            await this.context.Set<T>().AddAsync(entity);
            await SaveAllAsync();
            return entity;
        }

        public async Task<T> UpdateAsync(T entity)
        {
            this.context.Set<T>().Update(entity);
            await SaveAllAsync();
            return entity;
        }

        public async Task DeleteAsync(T entity)
        {
            //this.context.Set<T>().Remove(entity);
            entity.IsDeleted = true;
            this.context.Set<T>().Update(entity);
            await SaveAllAsync();
        }
        public async Task UnDeleteAsync(T entity)
        {
            entity.IsDeleted = false;
            this.context.Set<T>().Update(entity);
            await SaveAllAsync();
        }
        public async Task<bool> ExistAsync(int id)
        {
            return await this.context.Set<T>().AnyAsync(e => e.Id == id && e.IsDeleted == false);
        }

        public async Task<bool> SaveAllAsync()
        {
            return await this.context.SaveChangesAsync() > 0;
        }
    }
}
