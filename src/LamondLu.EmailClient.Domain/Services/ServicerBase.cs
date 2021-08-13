using LamondLu.EmailClient.Domain.Interface;

namespace LamondLu.EmailClient.Domain.Services
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
