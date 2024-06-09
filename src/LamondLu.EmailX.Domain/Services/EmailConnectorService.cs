using LamondLu.EmailX.Domain.Interface;
using LamondLu.EmailX.Domain.ViewModels;
using System;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;

namespace LamondLu.EmailX.Domain.Services
{
    public class EmailConnectorService : ServiceBase
    {
        public EmailConnectorService(IUnitOfWork unitOfWork) : base(unitOfWork)
        {

        }

        public async Task<List<EmailConnectorConfigViewModel>> GetEmailConnectors()
        {
            return await _unitOfWork.EmailConnectorRepository.GetEmailConnectors();
        }

        public async Task<EmailConnectorDetailViewModel> GetEmailConnector(Guid emailConnectorId)
        {
            throw new NotImplementedException();
        }
    }
}
