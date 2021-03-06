﻿using LamondLu.EmailClient.Domain.DTOs;
using LamondLu.EmailClient.Domain.Enum;
using LamondLu.EmailClient.Domain.Managers;
using LamondLu.EmailClient.Domain.Services;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Linq;
using System.Threading.Tasks;

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
                var result = await _emailConnectorManager.Add(model);

                if (result.Success)
                {
                    return RedirectToAction("Index");
                }
                else
                {
                    ModelState.AddModelError(result.Code.ToString(), result.Message);
                }
            }

            return View(model);
        }

        [HttpGet]
        [Route("Details")]
        public async Task<IActionResult> Details(Guid emailConnectorId)
        {
            var model = await _emailConnectorService.GetEmailConnector(emailConnectorId);

            return View(model);
        }

        [HttpPost]
        [Route("Update")]
        public async Task<IActionResult> Update()
        {
            if (ModelState.IsValid)
            {

            }

            return View();
        }

        [HttpPut]
        public async Task<IActionResult> Stop()
        {
            return Ok();
        }

        [HttpPut]
        public async Task<IActionResult> Start()
        {
            return Ok();
        }
    }
}