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

        public async Task<List<EmailConnectorConfigViewModel>> GetEmailConnectorConfigs()
        {
            return await _unitOfWork.EmailConnectorRepository.GetEmailConnectorConfigs();
        }

        public async Task<EmailConnector> GetEmailConnector(Guid emailConnectorId)
        {
            return await _unitOfWork.EmailConnectorRepository.GetEmailConnector(emailConnectorId);
        }
    }
}
