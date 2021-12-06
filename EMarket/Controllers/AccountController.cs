﻿using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;
using EMarket.Models;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Authentication.Google;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.ChangeTracking;

namespace EMarket.Controllers
{
    public enum Role
    {
        Seller, Buyer
    }

    public class AccountController : Controller
    {
        [Route("~/account/google-signin")]
        public IActionResult GoogleLogin() => 
            Challenge(
                new AuthenticationProperties
                {
                    RedirectUri = Url.Action("GoogleResponse")
                }, 
                GoogleDefaults.AuthenticationScheme);

        [Route("google-response")]
        public async Task<IActionResult> GoogleResponse()
        {
            var result = await HttpContext.AuthenticateAsync(CookieAuthenticationDefaults.AuthenticationScheme);
            var claims = result.Principal?.Identities.FirstOrDefault()?.Claims
                .ToDictionary(k => k.Type, v => v.Value);
            await using (var db = new AppContext())
            {
                Buyer user = await db.Buyers
                    .FirstOrDefaultAsync(p => p.Email == claims[ClaimTypes.Email]);
                if (user == null)
                {
                    await db.Buyers.AddAsync(new Buyer
                    {
                        Email = claims[ClaimTypes.Email],
                        FirstName = claims[ClaimTypes.GivenName],
                        LastName = claims[ClaimTypes.Surname],
                        Password = "jdkldnfoMNMe"
                    });
                    await db.SaveChangesAsync();
                }
            }

            return RedirectToAction("Index", "Home");
        }

        [HttpGet]
        public IActionResult Login()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Login(LoginModel model, Role role)
        {
            if (ModelState.IsValid)
            {
                await using AppContext db = new AppContext();
                if (role == Role.Buyer)
                {
                    Buyer user = await db.Buyers.FirstOrDefaultAsync(u => u.Email == model.Email 
                                                                          && u.Password == model.Password);
                    if (user != null)
                    {
                        await AuthenticateBuyer(user);
                        return RedirectToAction("Index", "Home");
                    }
                }
                else
                {
                    Seller user = await db.Sellers.FirstOrDefaultAsync(u => u.Email == model.Email
                                                                            && u.Password == model.Password);
                    if (user != null)
                    {
                        await AuthenticateSeller(user);
                        return RedirectToAction("Index", "Home");
                    }
                }
                ModelState.AddModelError("", "Incorrect login or/and password.");
            }
            return View("Login", model);
        }

        [HttpGet]
        public IActionResult RegisterAsBuyer()
        {
            return View("Register");
        }

        [HttpGet]
        public IActionResult RegisterAsSeller()
        {
            return View("SellerRegister");
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> RegisterAsBuyer(RegisterModel model)
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

                    await AuthenticateBuyer(addedBuyer.Entity);
                    return RedirectToAction("Index", "Home");
                }
                ModelState.AddModelError("", "Incorrect login or/and password.");
            }
            return View("Register", model);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> RegisterAsSeller(SellerRegisterModel model)
        {
            if (ModelState.IsValid)
            {
                await using AppContext db = new AppContext();
                Buyer user = await db.Buyers.FirstOrDefaultAsync(u => u.Email == model.Email);
                if (user == null)
                {
                    EntityEntry<Seller> addedSeller = await db.Sellers.AddAsync(new Seller
                    {
                        Email = model.Email,
                        Password = model.Password,
                        FirstName = model.FirstName,
                        LastName = model.LastName,
                        City = model.City
                    });
                    await db.SaveChangesAsync();

                    await AuthenticateSeller(addedSeller.Entity);
                    return RedirectToAction("Index", "Home");
                }
                ModelState.AddModelError("", "Incorrect login or/and password.");
            }
            return View("SellerRegister", model);
        }

        private async Task AuthenticateBuyer(Buyer buyer)
        {
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

        private async Task AuthenticateSeller(Seller seller)
        {
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

        public async Task<IActionResult> Logout()
        {
            await HttpContext.SignOutAsync(CookieAuthenticationDefaults.AuthenticationScheme);
            return RedirectToAction("Index", "Home");
        }
    }
}

