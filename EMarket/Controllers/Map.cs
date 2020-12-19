using GoogleApi;
using Microsoft.AspNetCore.Mvc;
using GoogleApi.Entities.Maps.Roads.SnapToRoads.Request;
using GoogleApi.Entities.Maps.Roads.SnapToRoads.Response;
using Microsoft.AspNetCore.Http;

namespace EMarket.Controllers
{
    public class Map : Controller
    {
        // GET
        public IActionResult PointsOfProduction()
        {
            SnapToRoadsRequest request = new SnapToRoadsRequest();
            SnapToRoadsResponse response;
                return View();
        }
    }
}