using MedVoll.Web.Models;
using Microsoft.AspNetCore.Authorization; //movido de Aula 1.4 para Aula 2.1
using Microsoft.AspNetCore.Mvc;
using System.Diagnostics;

namespace MedVoll.Web.Controllers
{
    [AllowAnonymous] //movido de Aula 1.4 para Aula 2.1
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;

        public HomeController(ILogger<HomeController> logger)
        {
            _logger = logger;
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
}