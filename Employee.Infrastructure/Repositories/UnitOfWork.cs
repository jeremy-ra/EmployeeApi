using System;
using System.Threading.Tasks;

namespace Employee.Infrastructure.Repositories
{
    public class UnitOfWork : IUnitOfWork
    {
        private readonly AppDbContext _appDbContext;

        public UnitOfWork(AppDbContext appDbContext)
        {
            _appDbContext = appDbContext;
        }        

        /// <summary>
        /// Saves or updates entity.
        /// </summary>
        /// <returns></returns>
        public Task<int> SaveChangesAsync()
        {
            return _appDbContext.SaveChangesAsync();
        }

        public void Dispose()
        {
            Dispose(true);
            GC.SuppressFinalize(this);
        }

        protected virtual void Dispose(bool disposing)
        {
            if (disposing)            
                _appDbContext.Dispose();
            
        }
    }
}
