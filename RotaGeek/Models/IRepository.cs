using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace RotaGeek.Models
{
    public interface IRepository<T> where T : class
    {
        Task<T> FindAsync(int id);
        Task<List<T>> GetAllAsync();
        Task SaveAsync();
        Task AddAsync(T entity);
        void Delete(T entity);
    }
}
