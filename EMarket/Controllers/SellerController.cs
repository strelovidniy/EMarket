using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;
using EMarket.Models;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using AppContext = EMarket.Models.AppContext;

namespace EMarket.Controllers
{
    //[Authorize(Roles = "Seller")]
    public class SellerController : Controller
    {
        public async Task<IActionResult> Index()
        {
            await using AppContext db = new AppContext();
            var orders = db.Orders.Where(o => o.SellerId == Int32.Parse(User.FindFirst("Id").Value))
                .Include(o => o.Buyer)
                .Include(o => o.Delivery)
                .Include(o => o.Destination)
                .Include(o => o.ProductOrder)
                .ThenInclude(po => po.Product).ToList();
            return View(orders);
        }

        [HttpGet]
        public async Task<IActionResult> AddProduct()
        {
            await using AppContext db = new AppContext();
            var categories = db.Categories.ToList();
            ViewBag.Categories = categories;
            
            return View(new Product());
        }

        [HttpPost]
        public async Task<IActionResult> AddProduct(Product product)
        {
            if (ModelState.IsValid)
            {
                await using AppContext db = new AppContext();
                product.SellerId = Int32.Parse(User.FindFirst("Id")?.Value ?? string.Empty);
                await db.Products.AddAsync(product);
                await db.SaveChangesAsync();
            }

            return RedirectToAction("Index", "Home");
        }

        [HttpGet]
        public IActionResult EditProfile()
        {
            return View(new Seller()
            {
                Email = User.FindFirst(u=>u.Type==ClaimTypes.Email)?.Value,
                FirstName = User.FindFirst(u=>u.Type==ClaimTypes.GivenName)?.Value,
                LastName = User.FindFirst(u=>u.Type==ClaimTypes.Surname)?.Value
            });
        }
        [HttpPost]
        public async Task<IActionResult> EditProfile(Seller seller)
        {
            if (ModelState.IsValid)
            {
                await using AppContext db = new AppContext();
                var user = await db.Sellers.FirstOrDefaultAsync(p =>
                    p.Email == User.FindFirst(u => u.Type == ClaimTypes.Email).Value);
                user.FirstName = seller.FirstName;
                user.LastName = seller.LastName;
                await db.SaveChangesAsync();
                await HttpContext.SignOutAsync(CookieAuthenticationDefaults.AuthenticationScheme);
                var claims = new List<Claim>
                {
                    new Claim(ClaimsIdentity.DefaultNameClaimType, seller.FirstName+" "+seller.LastName),
                    new Claim(ClaimTypes.Email, seller.Email),
                    new Claim(ClaimTypes.GivenName, seller.FirstName),
                    new Claim(ClaimTypes.Surname, seller.LastName),
                    new Claim(ClaimTypes.Locality, seller.City),
                    new Claim(ClaimTypes.Role, "Seller")
                };
                ClaimsIdentity id = new ClaimsIdentity(claims, "ApplicationCookie", ClaimsIdentity.DefaultNameClaimType,
                    "Seller");
                await HttpContext.SignInAsync(CookieAuthenticationDefaults.AuthenticationScheme, new ClaimsPrincipal(id));
            }

            return RedirectToAction("Index", "Home");
        }
    }
}
