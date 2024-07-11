
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

        [Route("Emails")]
        public async Task<IActionResult> Index(Guid? emailConnectorId = null)
        {
            ViewBag.EmailConnectorId = emailConnectorId;
            var emailConnectors = await _unitOfWork.EmailConnectorRepository.GetEmailConnectorConfigs();

            return View(emailConnectors);
        }

        [Route("Emails/{id}")]
        public async Task<IActionResult> Details(Guid id)
        {
            var email = await _unitOfWork.EmailRepository.GetEmail(id);
            return View(email);
        }

        [Route("Emails/{id}/Content")]
        public async Task<IActionResult> Content(Guid id)
        {
            var email = await _unitOfWork.EmailRepository.GetEmailBody(id);

            return new ContentResult
            {
                Content = email,
                ContentType = "text/html"
            };

        }
    }
}