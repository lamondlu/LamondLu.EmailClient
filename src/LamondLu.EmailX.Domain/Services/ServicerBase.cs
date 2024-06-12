using LamondLu.EmailX.Domain.Interface;

namespace LamondLu.EmailX.Domain.Services
{
    public abstract class ServiceBase
    {
        protected IUnitOfWork _unitOfWork = null;

        public ServiceBase(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

         
    }
}
