using LamondLu.EmailClient.Domain.Interface;
using System;
using System.Collections.Generic;
using System.Text;

namespace LamondLu.EmailClient.Domain.Services
{
    public abstract class ServicerBase
    {
        protected IUnitOfWork _unitOfWork = null;

        public ServicerBase(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

         
    }
}
