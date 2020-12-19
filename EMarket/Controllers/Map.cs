using Microsoft.AspNetCore.Mvc;
namespace EMarket.Controllers
{
    public class Map : Controller
    {
        // GET
        public IActionResult PointsOfProduction()
        {
            
            return View();
        }
    }
}