using LamondLu.EmailX.Domain.Interface;
using Microsoft.AspNetCore.Mvc;

namespace LamondLu.EmailX.Server.Controllers
{
    [Route("api/emailconnectors")]
    public class EmailConnectorController : ControllerBase
    {
        private IUnitOfWork _unitOfWork = null;

        public EmailConnectorController(IUnitOfWorkFactory unitOfWorkFactory)
        {
            _unitOfWork = unitOfWorkFactory.Create();
        }

        // GET: api/EmailConnector
        [HttpGet("")]
        public async Task<IActionResult> Get()
        {
            return Ok(await _unitOfWork.EmailConnectorRepository.GetEmailConnectors());
        }
    }
}