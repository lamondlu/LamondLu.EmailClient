using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using LamondLu.EmailX.Domain.Interface;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;

namespace LamondLu.EmailX.Portal.Controllers
{
    public class EmailConnectorController : Controller
    {
        private readonly ILogger<EmailConnectorController> _logger;
        private IUnitOfWork _unitOfWork = null;

        public EmailConnectorController(ILogger<EmailConnectorController> logger, IUnitOfWorkFactory unitOfWorkFactory)
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