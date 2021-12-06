using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;
using EMarket.Models;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace EMarket.Controllers
{
    public class BuyerController : Controller
    {
        public async Task<IActionResult> Index()
        {
            await using AppContext db = new AppContext();
            var orders = db.Orders.Include(s=>s.Buyer)
                .Where(o => o.Buyer.Email == User.FindFirst(u 
                    => u.Type == ClaimTypes.Email).Value)
                .Include(o => o.Seller)
                .Include(o => o.Delivery)
                .Include(o => o.ProductOrder)
                .ThenInclude(po => po.Product).ToList();
            return View(orders);
        }

        public async Task<IActionResult> Account()
        {
            await using AppContext db = new AppContext();
            Buyer buyer = await db.Buyers.FirstOrDefaultAsync(b => b.Email ==
                                                                   User.FindFirst(u => u.Type == ClaimTypes.Email)
                                                                       .Value);
            return View(buyer);
        }

        [HttpGet]
        public IActionResult EditBuyer()
        {
            return View(new Buyer
            {
                FirstName = User.FindFirst(u => u.Type == ClaimTypes.GivenName).Value,
                LastName = User.FindFirst(u => u.Type == ClaimTypes.Surname).Value
            });
        }

        [HttpPost]
        public async Task<IActionResult> EditBuyer(Buyer buyer)
        {
            if (ModelState.IsValid)
            {
                await using AppContext db = new AppContext();
                string email = User?.FindFirst(u => u.Type == ClaimTypes.Email)?.Value;
                var user = await db.Buyers.FirstOrDefaultAsync(p =>
                    p.Email == email);
                user.FirstName = buyer.FirstName;
                user.LastName = buyer.LastName;
                await db.SaveChangesAsync();
                await HttpContext.SignOutAsync(CookieAuthenticationDefaults.AuthenticationScheme);

                var claims = new List<Claim>
                {
                    new Claim(ClaimsIdentity.DefaultNameClaimType, buyer.FirstName + " " + buyer.LastName),
                    new Claim(ClaimTypes.Email, email),
                    new Claim(ClaimTypes.GivenName, buyer.FirstName),
                    new Claim(ClaimTypes.Surname, buyer.LastName),
                    new Claim(ClaimTypes.Role, "Buyer")
                };
                ClaimsIdentity id = new ClaimsIdentity(claims, "ApplicationCookie", ClaimsIdentity.DefaultNameClaimType,
                    "Buyer");
                await HttpContext.SignInAsync(CookieAuthenticationDefaults.AuthenticationScheme, new ClaimsPrincipal(id));
            }
            return RedirectToAction("Index", "Home");
        }
    }
}