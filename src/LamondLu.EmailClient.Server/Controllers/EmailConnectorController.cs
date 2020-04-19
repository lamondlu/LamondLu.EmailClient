using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using LamondLu.EmailClient.Domain.Interface;
using LamondLu.EmailClient.Domain.Services;
using Microsoft.AspNetCore.Mvc;

namespace LamondLu.EmailClient.Server.Controllers
{
    public class EmailConnectorController : Controller
    {
        private EmailConnectorService _emailConnectorService = null;

        public EmailConnectorController(EmailConnectorService emailConnectorService)
        {
            _emailConnectorService = emailConnectorService;
        }

        public async Task<IActionResult> Index()
        {
            var connectors = await _emailConnectorService.GetEmailConnectors();

            return View(connectors);
        }
    }
}