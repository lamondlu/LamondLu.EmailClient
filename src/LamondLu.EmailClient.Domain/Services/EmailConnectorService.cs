﻿using LamondLu.EmailClient.Domain.Interface;
using LamondLu.EmailClient.Domain.ViewModels;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace LamondLu.EmailClient.Domain.Services
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
