using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;

namespace EMarket.Controllers
{
    public class SellerController : Controller
    {
        [Authorize(Roles = "Seller")]
        public IActionResult Index()
        {
            return View();
        }
    }
}
