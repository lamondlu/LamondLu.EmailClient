using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using LamondLu.EmailClient.Domain.DTOs;
using LamondLu.EmailClient.Domain.Enum;
using LamondLu.EmailClient.Domain.Interface;
using LamondLu.EmailClient.Domain.Managers;
using LamondLu.EmailClient.Domain.Services;
using Microsoft.AspNetCore.Mvc;

namespace LamondLu.EmailClient.Server.Controllers
{
    public class EmailConnectorController : Controller
    {
        private EmailConnectorService _emailConnectorService = null;
        private EmailConnectorManager _emailConnectorManager = null;

        public EmailConnectorController(EmailConnectorService emailConnectorService, EmailConnectorManager emailConnectorManager)
        {
            _emailConnectorService = emailConnectorService;
            _emailConnectorManager = emailConnectorManager;
        }

        [Route("List")]
        public async Task<IActionResult> Index()
        {
            var connectors = await _emailConnectorService.GetEmailConnectors();
            return View(connectors);
        }

        [HttpGet]
        [Route("Add")]
        public async Task<IActionResult> Add()
        {
            ViewBag.Types = Enum.GetValues(typeof(EmailConnectorType)).Cast<EmailConnectorType>();

            return View();
        }

        [HttpPost]
        [Route("Add")]
        public async Task<IActionResult> Add(AddEmailConnectorModel model)
        {
            if (ModelState.IsValid)
            {
                await _emailConnectorManager.Add(model);

                return RedirectToAction("Index");
            }

            return View(model);
        }

        [HttpGet]
        [Route("Details")]
        public async Task<IActionResult> Details(Guid emailConnectorId)
        {
            return View();
        }


    }
}