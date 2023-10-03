using Employee.Infrastructure.Repositories;

namespace Employee.Service.Services
{
    public abstract class BaseService
    {
        protected readonly IUnitOfWork _unitOfWork;

        protected BaseService(IUnitOfWork unitOfWork) 
        {
            _unitOfWork = unitOfWork;
        }
    }
}
