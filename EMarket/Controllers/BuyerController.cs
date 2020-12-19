using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using EMarket.Models;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.EntityFrameworkCore;

namespace EMarket.Controllers
{
    public class BuyerController : Controller
    {
        [HttpGet]
        public IActionResult EditBuyer()
        {
            return View(new Buyer(){Email = User.FindFirst(u => u.Type == ClaimTypes.Email).Value,
                                            FirstName = User.FindFirst(u => u.Type == ClaimTypes.GivenName).Value,
                                            LastName  = User.FindFirst(u => u.Type == ClaimTypes.Surname).Value});
        }
        [HttpPost]
        public async Task<IActionResult> EditBuyer(Buyer buyer)
        {
            if (ModelState.IsValid)
            {
                await using AppContext db = new AppContext();
                var user = await db.Buyers.FirstOrDefaultAsync(p =>
                    p.Email == User.FindFirst(u => u.Type == ClaimTypes.Email).Value);
                user.FirstName = buyer.FirstName;
                user.LastName = buyer.LastName;
                await db.SaveChangesAsync();
                await HttpContext.SignOutAsync(CookieAuthenticationDefaults.AuthenticationScheme);
                
                var claims = new List<Claim>
                {
                    new Claim(ClaimsIdentity.DefaultNameClaimType, buyer.FirstName+" "+buyer.LastName),
                    new Claim(ClaimTypes.Email, buyer.Email),
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