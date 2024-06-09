
using LamondLu.EmailX.Domain.Interface;
using Microsoft.AspNetCore.Mvc;

namespace LamondLu.EmailX.Portal.Controllers
{
    public class EmailController : Controller
    {
        private readonly ILogger<EmailController> _logger;
        private IUnitOfWork _unitOfWork;

        public EmailController(ILogger<EmailController> logger, IUnitOfWorkFactory unitOfWorkFactory)
        {
            _logger = logger;
            _unitOfWork = unitOfWorkFactory.Create();
        }

        public async Task<IActionResult> Index(Guid? emailConnectorId = null)
        {
            ViewBag.EmailConnectorId = emailConnectorId;
            var emailConnectors = await _unitOfWork.EmailConnectorRepository.GetEmailConnectors();

            return View(emailConnectors);
        }
    }
}