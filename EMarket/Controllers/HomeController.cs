using System.Diagnostics;
using EMarket.Models;
using EMarket.Services;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;

namespace EMarket.Controllers
{

    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;

        ISearchService searchService = new SearchService();
        public IActionResult Index(string query)
        {
            if (query != null)
            {
                return View(searchService.GetAllProductsByName(query));
            }
            return View(searchService.GetAll());
        }
        public HomeController(ILogger<HomeController> logger)
        {
            _logger = logger;
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}
