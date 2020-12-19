using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;
using EMarket.Models;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.ChangeTracking;
using AppContext = EMarket.Models.AppContext;

namespace EMarket.Controllers
{

    public class AccountController : Controller
    {
        [HttpGet]
        public IActionResult Login()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Login(LoginModel model)
        {
            if (ModelState.IsValid)
            {
                await using AppContext db = new AppContext();
                Buyer user = await db.Buyers.FirstOrDefaultAsync(u => u.Email == model.Email && u.Password == model.Password);
                if (user != null)
                {
                    await Authenticate(user);
                    return RedirectToAction("Index", "Home");
                }
                ModelState.AddModelError("", "Некорректные логин и(или) пароль");
            }
            return View(model);
        }

        [HttpGet]
        public IActionResult Register()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Register(RegisterModel model)
        {
            if (ModelState.IsValid)
            {
                await using AppContext db = new AppContext();
                Buyer user = await db.Buyers.FirstOrDefaultAsync(u => u.Email == model.Email);
                if (user == null)
                {
                    EntityEntry<Buyer> addedBuyer = await db.Buyers.AddAsync(new Buyer
                    {
                        Email = model.Email,
                        Password = model.Password,
                        FirstName = model.FirstName,
                        LastName = model.LastName
                    });
                    await db.SaveChangesAsync();

                    await Authenticate(addedBuyer.Entity);
                    return RedirectToAction("Index", "Home");
                }
                else
                    ModelState.AddModelError("", "Некорректные логин и(или) пароль");
            }
            return View(model);
        }

        private async Task Authenticate(Buyer buyer)
        {
            var claims = new List<Claim>
            {
                new Claim(ClaimsIdentity.DefaultNameClaimType, buyer.FirstName+" "+buyer.LastName),
                new Claim("id", buyer.Id.ToString()),
                new Claim(ClaimTypes.Email, buyer.Email),
                new Claim(ClaimTypes.Name, buyer.FirstName+" "+buyer.LastName)
            };
            ClaimsIdentity id = new ClaimsIdentity(claims, "ApplicationCookie", ClaimsIdentity.DefaultNameClaimType,
                ClaimsIdentity.DefaultRoleClaimType);
            await HttpContext.SignInAsync(CookieAuthenticationDefaults.AuthenticationScheme, new ClaimsPrincipal(id));
        }

        public async Task<IActionResult> Logout()
        {
            await HttpContext.SignOutAsync(CookieAuthenticationDefaults.AuthenticationScheme);
            return RedirectToAction("Login", "Account");
        }
    }
}

