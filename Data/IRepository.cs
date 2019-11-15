using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Data
{
    public interface IRepository<T> where T : class, IEntity
    {
        Task<List<T>> GetAll();
        Task<T> Get(string userId);
        Task<T> Get(Guid id);
        //Task<T> Get(string userId, Guid id);
        Task Add(T entity);
        Task Delete(Guid id);
        Task Update(T entity);
    }
}
