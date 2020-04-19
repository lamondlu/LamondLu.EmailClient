using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using LamondLu.EmailClient.Domain.Interface;
using Microsoft.AspNetCore.Mvc;

namespace LamondLu.EmailClient.Server.Controllers
{
    public class EmailConnectorController : Controller
    {
        private IUnitOfWork _unitOfWork = null;

        public EmailConnectorController(IUnitOfWorkFactory unitOfWorkFactory)
        {
            _unitOfWork = unitOfWorkFactory.Create();
        }

        public async Task<IActionResult> Index()
        {
            var connectors = await _unitOfWork.EmailConnectorRepository.GetEmailConnectors();

            return View(connectors);
        }
    }
}