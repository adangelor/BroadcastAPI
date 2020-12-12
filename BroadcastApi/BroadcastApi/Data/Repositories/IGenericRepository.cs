using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BroadcastApi.Data.Repositories
{
    public interface IGenericRepository<T> where T : class
    {
        IQueryable<T> GetAll();
        IQueryable<T> GetAllDeleted();

        Task<T> GetByIdAsync(int id);
        Task<T> GetDeletedByIdAsync(int id);

        Task<T> CreateAsync(T entity);

        Task<T> UpdateAsync(T entity);

        Task DeleteAsync(T entity);
        Task UnDeleteAsync(T entity);

        Task<bool> ExistAsync(int id);

    }
}
