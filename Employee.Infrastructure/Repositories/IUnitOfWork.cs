using System;
using System.Threading.Tasks;

namespace Employee.Infrastructure.Repositories
{
    public interface IUnitOfWork : IDisposable
    {
        Task<int> SaveChangesAsync();
    }
}
