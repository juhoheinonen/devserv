using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DevServ.SharedKernel.Interfaces
{
    public interface IRepository<T> where T : BaseEntity, IAggregateRoot
    {
        Task<T> GetByIdAsync(int id);
        Task<List<T>> ListAsync();
        //Task<List<T>> ListAsync<T>(ISpecification<T> spec) where T : BaseEntity, IAggregateRoot; 
        //Task<T> AddAsync<T>(T entity) where T : BaseEntity, IAggregateRoot;
        //Task UpdateAsync<T>(T entity) where T : BaseEntity, IAggregateRoot;
        //Task DeleteAsync<T>(T entity) where T : BaseEntity, IAggregateRoot;
    }
}
