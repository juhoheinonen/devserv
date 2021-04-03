using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DevServ.SharedKernel.Interfaces
{
    public interface IRepository<T> where T : BaseEntity
    {
        Task<T> GetByIdAsync(int id);
        Task<List<T>> ListAsync();
        Task<List<T>> ListAsync(IFilter<T> filter);
        Task<T> AddAsync(T entity);
        Task UpdateAsync(T entity);
        Task DeleteAsync(int id);
    }
}
