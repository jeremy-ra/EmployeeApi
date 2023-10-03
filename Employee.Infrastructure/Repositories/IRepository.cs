using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Employee.Infrastructure.Repositories
{
    public interface IRepository<T> where T : class
    {
        void Add(T entity);
        void Update(T entity);
        Task<IEnumerable<T>> GetAll();
        Task<T> GetById(int id);
    }
}
