using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;
using EMarket.Models;
using EMarket.Services;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using AppContext = EMarket.Models.AppContext;

namespace EMarket.Controllers
{
    public class SellerController : Controller
    {
        ISearchService searchService = new SearchService();
        public async Task<IActionResult> Index()
        {
            await using AppContext db = new AppContext();
            string email = User.FindFirst(u
                => u.Type == ClaimTypes.Email)!.Value;
            var orders = db.Orders.Include(s => s.Seller)
                ?.Where(o => o.Seller.Email == email)
                .Include(o => o.Buyer)
                .Include(o => o.Delivery)
                .Include(o => o.ProductOrder)
                .ThenInclude(po => po.Product).ToList();
            if (orders == null)
                return View(new List<Order>());
            return View(orders);
        }

        [HttpGet]
        public IActionResult AddProduct()
        {
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
        public IActionResult EditSeller()
        {
            return View(new Seller()
            {
                Email = User.FindFirst(u => u.Type == ClaimTypes.Email)?.Value,
                FirstName = User.FindFirst(u => u.Type == ClaimTypes.GivenName)?.Value,
                LastName = User.FindFirst(u => u.Type == ClaimTypes.Surname)?.Value
            });
        }

        [HttpPost]
        public async Task<IActionResult> EditSeller(Seller seller)
        {
            if (ModelState.IsValid)
            {
                await using AppContext db = new AppContext();
                string email = User?.FindFirst(u => u.Type == ClaimTypes.Email)?.Value;
                var user = await db.Sellers.FirstOrDefaultAsync(p =>
                    p.Email == email);
                user.FirstName = seller.FirstName;
                user.LastName = seller.LastName;
                user.City = seller.City;
                await db.SaveChangesAsync();
                await HttpContext.SignOutAsync(CookieAuthenticationDefaults.AuthenticationScheme);
                var claims = new List<Claim>
                {
                    new Claim(ClaimsIdentity.DefaultNameClaimType, seller.FirstName+" "+seller.LastName),
                    new Claim(ClaimTypes.Email, email),
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

        [HttpGet]
        public async Task<IActionResult> EditProduct(int id)
        {
            await using AppContext db = new AppContext();
            string email = User.FindFirst(u => u.Type == ClaimTypes.Email).Value;
            Product? product = db.Products.Include(p => p.Seller).AsEnumerable()
                .FirstOrDefault(p => p.Id == id && p.Seller.Email == email);
            if (product == null)
                return View("Error", new ErrorViewModel() { RequestId = "404 Not Found." });
            return View(product);
        }

        [HttpPost]
        public async Task<IActionResult> EditProduct(Product product)
        {
            await using AppContext db = new AppContext();
            string email = User?.FindFirst(u => u.Type == ClaimTypes.Email)?.Value;
            var productToEdit = await db.Products.FirstOrDefaultAsync(p => p.Id == product.Id);
            productToEdit.Count = product.Count;
            productToEdit.Description = product.Description;
            productToEdit.Quantity = product.Quantity;
            productToEdit.Price = product.Price;
            productToEdit.Name = product.Name;
            await db.SaveChangesAsync();
            return View("Products");
        }

        public async Task<IActionResult> Products()
        {
            await using AppContext db = new AppContext();
            string email = User?.FindFirst(u => u.Type == ClaimTypes.Email)?.Value;
            var products = db.Products.Include(p => p.Seller)
                .Where(p => p.Seller.Email == email).ToList();
            return View(products);
        }
    }
}
