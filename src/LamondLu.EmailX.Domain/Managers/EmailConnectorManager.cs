using LamondLu.EmailX.Domain.DTOs;
using LamondLu.EmailX.Domain.Interface;
using LamondLu.EmailX.Domain.Models;
using LamondLu.EmailX.Domain.Results;
using System;
using System.Threading.Tasks;

namespace LamondLu.EmailX.Domain.Managers
{
    public class EmailConnectorManager : ManagerBase
    {
        private IUnitOfWork _unitOfWork = null;

        public EmailConnectorManager(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        public async Task<OperationResult> Add(AddEmailConnectorModel model)
        {
            try
            {
                var emailConnector = new EmailConnector(model.Name, model.EmailAddress, model.UserName, model.Password, new EmailServerConfig(model.Server, model.Port, model.EnableSSL), model.Type, model.Description);

                var duplicateChecking = await _unitOfWork.EmailConnectorRepository.CheckDuplicated(emailConnector.EmailAddress, emailConnector.Name, emailConnector.EmailConnectorId);

                if (!duplicateChecking)
                {
                    //TODO: we need to validate whether the email setting are correct

                    await _unitOfWork.EmailConnectorRepository.AddEmailConnector(emailConnector);
                    var result = await _unitOfWork.SaveAsync();

                    if (result.Success)
                    {
                        return OperationResult.SuccessResult;
                    }
                    else
                    {
                        return new UnexpectedErrorResult(result.Error.Message);
                    }
                }
                else
                {
                    return new DuplicateEmailConnectorResult();
                }
            }
            catch (Exception ex)
            {
                return new UnexpectedErrorResult(ex.Message);
            }
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
