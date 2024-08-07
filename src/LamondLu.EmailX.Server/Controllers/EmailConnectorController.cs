using LamondLu.EmailX.Domain;
using LamondLu.EmailX.Domain.DTOs;
using LamondLu.EmailX.Domain.Enum;
using LamondLu.EmailX.Domain.Interface;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using LamondLu.Core.Api;
using LamondLu.EmailX.Domain.Models.EmailConnectors;
using LamondLu.EmailX.Domain.Managers;
using Microsoft.AspNetCore.DataProtection.XmlEncryption;

namespace LamondLu.EmailX.Server.Controllers
{
    [Route("api/emailconnectors")]
    public class EmailConnectorController : ControllerBase
    {
        private IUnitOfWork _unitOfWork;

        private EmailConnectorManager _emailConnectorManager;

        private IEncrypt _encryptor;

        public EmailConnectorController(IUnitOfWorkFactory unitOfWorkFactory, EmailConnectorManager emailConnectorManager, IEncrypt encryptor)
        {
            _unitOfWork = unitOfWorkFactory.Create();
            _emailConnectorManager = emailConnectorManager;
            _encryptor = encryptor;
        }

        // GET: api/EmailConnector
        [HttpGet("")]
        public async Task<IActionResult> Get()
        {
            var result = await _unitOfWork.EmailConnectorRepository.GetEmailConnectorStatuses();

            return Ok(
                new SuccessResponse(result));
        }

        [HttpPost("")]
        public async Task<IActionResult> Add([FromBody] AddEmailConnectorModel model)
        {
            Console.WriteLine("Add Email Connector");
            Console.WriteLine(JsonConvert.SerializeObject(model));

            if (await _unitOfWork.EmailConnectorRepository.CheckDuplicated(model.EmailAddress, model.Name, Guid.Empty))
            {
                return BadRequest("Duplicated Email Connector");
            }

            await _unitOfWork.EmailConnectorRepository.AddEmailConnector(new EmailConnector(
                model.Name,
                model.EmailAddress,
                model.UserName,
                _encryptor.Encrypt(model.Password),
                new EmailServerConfig(
                    model.SMTPServer,
                    model.SMTPPort,
                    model.IMAPServer,
                    model.IMAPPort,
                    model.POP3Server,
                    model.POP3Port,
                    model.EnableSSL
                ),
                model.Type,
                model.Description
            ));

            await _unitOfWork.SaveAsync();

            return Ok(new SuccessResponse());
        }

        [HttpPut("{id}/status")]
        public async Task<IActionResult> ChangeStatus(Guid id, [FromBody] EmailConnectorStatusChangedModel model)
        {
            if (!ModelState.IsValid)
            {
                return UnprocessableEntity();
            }

            if (model.Status == EmailConnectorStatus.Stopped)
            {
                await _emailConnectorManager.StopAsync(id);
                return Ok(new SuccessResponse());
            }
            else
            {
                await _emailConnectorManager.StartAsync(id);
                return Ok(new SuccessResponse());
            }
        }
    }
}