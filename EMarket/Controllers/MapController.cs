using EMarket.Services;
using Microsoft.AspNetCore.Mvc;

namespace EMarket.Controllers
{
    public class MapController : Controller
    {
        IGeolocationService geoService = new GeolocationService();
        public IActionResult PointsOfProduction()
        {
            ViewBag.Locations = geoService.GetGeolocations();
            
            return View();
        }
    }
}