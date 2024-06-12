using System.Diagnostics;
using Microsoft.AspNetCore.Mvc;
using LamondLu.EmailX.Portal.Models;
using LamondLu.EmailX.Domain.Interface;

namespace LamondLu.EmailX.Portal.Controllers;

public class HomeController : Controller
{
    private readonly ILogger<HomeController> _logger;
    private IUnitOfWork _unitOfWork;

    public HomeController(ILogger<HomeController> logger, IUnitOfWorkFactory unitOfWorkFactory)
    {
        _logger = logger;
        _unitOfWork = unitOfWorkFactory.Create();
    }

    public IActionResult Index()
    {
        return View();
    }

    public IActionResult Privacy()
    {
        return View();
    }

    [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
    public IActionResult Error()
    {
        return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
    }
}
