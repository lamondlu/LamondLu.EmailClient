using LamondLu.EmailX.Domain.Interface;
using Microsoft.AspNetCore.Mvc;

namespace LamondLu.EmailX.Portal.Controllers
{
    public class EmailConnectorController : Controller
    {
        private readonly ILogger<EmailConnectorController> _logger;
        private IUnitOfWork _unitOfWork;

        public EmailConnectorController(ILogger<EmailConnectorController> logger, IUnitOfWorkFactory unitOfWorkFactory)
        {
            _logger = logger;
            _unitOfWork = unitOfWorkFactory.Create();
        }

        public async Task<IActionResult> Index()
        {
            var emailConnectors = await _unitOfWork.EmailConnectorRepository.GetEmailConnectorConfigs();

            return View(emailConnectors);
        }
    }
}