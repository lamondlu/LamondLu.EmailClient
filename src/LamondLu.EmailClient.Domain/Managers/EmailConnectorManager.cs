using LamondLu.EmailClient.Domain.DTOs;
using LamondLu.EmailClient.Domain.Interface;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace LamondLu.EmailClient.Domain.Managers
{
    public class EmailConnectorManager : ManagerBase
    {
        private IUnitOfWork _unitOfWork = null;

        public EmailConnectorManager(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        public async Task Add(AddEmailConnectorModel model)
        {
            var emailConnector = new EmailConnector(model.Name, model.EmailAddress, model.UserName, model.Password, new EmailServerConfig(model.Server, model.Port, model.EnableSSL), model.Type, model.Description);

            var duplicateChecking = await _unitOfWork.EmailConnectorRepository.CheckDuplicated(emailConnector.EmailAddress, emailConnector.Name, emailConnector.EmailConnectorId);

            //TODO: we need to validate whether the email setting are correct

            await _unitOfWork.EmailConnectorRepository.AddEmailConnector(emailConnector);
            await _unitOfWork.SaveAsync();
        }

        public void Update(Guid emailConnectorId)
        {

        }

        public void Delete(Guid emailConnectorId)
        {

        }

        public void Stop(Guid emailConnectorId)
        {

        }

        public void Start(Guid emailConnectorId)
        {

        }
    }
}
