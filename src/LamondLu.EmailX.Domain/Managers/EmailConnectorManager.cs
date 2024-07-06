using LamondLu.EmailX.Domain.DTOs;
using LamondLu.EmailX.Domain.Interface;
using LamondLu.EmailX.Domain.Models;
using LamondLu.EmailX.Domain.Results;
using Microsoft.Extensions.Logging;
using System;
using System.Threading.Tasks;

namespace LamondLu.EmailX.Domain.Managers
{
    public class EmailConnectorManager
    {
        private IUnitOfWork _unitOfWork = null;
        private readonly ILogger<EmailConnectorManager> _logger;

        public EmailConnectorManager(IUnitOfWorkFactory unitOfWorkFactory, ILogger<EmailConnectorManager> logger)
        {
            _unitOfWork = unitOfWorkFactory.Create();
            _logger = logger;
        }

        public async Task<OperationResult> Add(AddEmailConnectorModel model)
        {
            try
            {
                var emailConnector = new EmailConnector(model.Name, model.EmailAddress, model.UserName, model.Password, new EmailServerConfig(model.SMTPServer, model.SMTPPort, model.IMAPServer, model.IMAPPort, model.POP3Server, model.POP3Port, model.EnableSSL), model.Type, model.Description);

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

        public async Task StopAsync(Guid emailConnectorId)
        {
            _logger.LogInformation($"System is stopping email connector {emailConnectorId}");
        }

        public async Task StartAsync(Guid emailConnectorId)
        {
            _logger.LogInformation($"System is starting email connector {emailConnectorId}");
        }
    }
}
